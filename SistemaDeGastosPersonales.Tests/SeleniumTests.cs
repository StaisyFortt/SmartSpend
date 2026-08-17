using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace SistemaDeGastosPersonales.Tests
{
    public class TestResult
    {
        public string Name { get; set; }
        public bool Passed { get; set; }
        public string ErrorMessage { get; set; }
        public string ScreenshotPath { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class SeleniumTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl = "http://localhost:5104";
        private static readonly List<TestResult> _resultados = new List<TestResult>();
        private static readonly object _lock = new object();

        public SeleniumTests()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        private void EjecutarTest(string nombreTest, Action accion)
        {
            bool paso = false;
            string error = null;
            string screenshotRelativo = null;

            try
            {
                accion();
                paso = true;
                screenshotRelativo = TomarScreenshot(nombreTest);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                screenshotRelativo = TomarScreenshot(nombreTest + "_Error");
                throw;
            }
            finally
            {
                lock (_lock)
                {
                    var existente = _resultados.FirstOrDefault(r => r.Name == nombreTest);
                    if (existente != null)
                    {
                        existente.Passed = paso;
                        existente.ErrorMessage = error;
                        existente.ScreenshotPath = screenshotRelativo;
                        existente.Timestamp = DateTime.Now;
                    }
                    else
                    {
                        _resultados.Add(new TestResult
                        {
                            Name = nombreTest,
                            Passed = paso,
                            ErrorMessage = error,
                            ScreenshotPath = screenshotRelativo,
                            Timestamp = DateTime.Now
                        });
                    }
                    GenerarReporteHtml();
                }
            }
        }

        private string TomarScreenshot(string nombreTest)
        {
            try
            {
                var screenshotDriver = _driver as ITakesScreenshot;
                if (screenshotDriver != null)
                {
                    var screenshot = screenshotDriver.GetScreenshot();
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    string reportsDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "SmartSpendReports"));
                    string screenshotsDir = Path.Combine(reportsDir, "screenshots");
                    Directory.CreateDirectory(screenshotsDir);

                    string fileName = $"{nombreTest}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    string filePath = Path.Combine(screenshotsDir, fileName);
                    screenshot.SaveAsFile(filePath);
                    return Path.Combine("screenshots", fileName);
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        private static void GenerarReporteHtml()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string reportsDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "SmartSpendReports"));
            Directory.CreateDirectory(reportsDir);
            string htmlPath = Path.Combine(reportsDir, "index.html");

            int total = _resultados.Count;
            int passed = _resultados.Count(r => r.Passed);
            int failed = _resultados.Count(r => !r.Passed);

            string rows = "";
            foreach (var res in _resultados)
            {
                string badge = res.Passed
                    ? "<span class='badge bg-success'>PASÓ</span>"
                    : "<span class='badge bg-danger'>FALLÓ</span>";

                string screenshotHtml = !string.IsNullOrEmpty(res.ScreenshotPath)
                    ? $"<a href='{res.ScreenshotPath}' target='_blank'><img src='{res.ScreenshotPath}' class='img-thumbnail' style='max-width:180px;'/></a>"
                    : "<span class='text-muted small'>Sin captura</span>";

                string errorHtml = !res.Passed
                    ? $"<div class='alert alert-danger p-2 mt-1 small' style='font-size:0.8rem;'>{res.ErrorMessage}</div>"
                    : "";

                rows += $@"
                <tr>
                    <td>{res.Timestamp:HH:mm:ss}</td>
                    <td><strong>{res.Name}</strong></td>
                    <td>{badge}</td>
                    <td>{errorHtml}{screenshotHtml}</td>
                </tr>";
            }

            string htmlContent = $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <title>Reporte de Ejecución - SmartSpend</title>
    <link href='https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css' rel='stylesheet'>
    <style>
        body {{ background-color: #f4f6f9; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; }}
        .header {{ background: linear-gradient(135deg, #1a2a6c 0%, #b21f1f 50%, #fdbb2d 100%); color: white; padding: 25px; border-radius: 8px; margin-bottom: 25px; box-shadow: 0 4px 15px rgba(0,0,0,0.15); }}
        .card {{ border: none; box-shadow: 0 4px 8px rgba(0,0,0,0.05); border-radius: 10px; }}
        .table {{ margin-bottom: 0; }}
        .badge {{ font-size: 0.85rem; padding: 6px 10px; }}
    </style>
</head>
<body class='container my-4'>
    <div class='header text-center'>
        <h1 class='fw-bold'>Reporte de Pruebas Automatizadas</h1>
        <p class='mb-0 opacity-75'>SmartSpend - Sistema de Control de Gastos Personales</p>
    </div>
    <div class='row mb-4'>
        <div class='col-md-4'>
            <div class='card text-center bg-primary text-white p-3'>
                <h3 class='fw-bold'>{total}</h3>
                <span>Total Pruebas</span>
            </div>
        </div>
        <div class='col-md-4'>
            <div class='card text-center bg-success text-white p-3'>
                <h3 class='fw-bold'>{passed}</h3>
                <span>Aprobadas</span>
            </div>
        </div>
        <div class='col-md-4'>
            <div class='card text-center bg-danger text-white p-3'>
                <h3 class='fw-bold'>{failed}</h3>
                <span>Fallidas</span>
            </div>
        </div>
    </div>
    <div class='card p-4'>
        <table class='table table-hover align-middle'>
            <thead>
                <tr class='table-light'>
                    <th style='width: 15%'>Hora</th>
                    <th style='width: 35%'>Caso de Prueba</th>
                    <th style='width: 15%'>Estado</th>
                    <th style='width: 35%'>Evidencia</th>
                </tr>
            </thead>
            <tbody>
                {rows}
            </tbody>
        </table>
    </div>
</body>
</html>";

            File.WriteAllText(htmlPath, htmlContent);
        }

        [Fact]
        public void Login_CaminoFeliz_InicioSesionExitoso()
        {
            EjecutarTest("Login_CaminoFeliz_InicioSesionExitoso", () =>
            {
                _driver.Navigate().GoToUrl(_baseUrl + "/index.html");

                var emailInput = _driver.FindElement(By.Id("email"));
                var passwordInput = _driver.FindElement(By.Id("password"));
                var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));

                emailInput.SendKeys(Keys.Control + "a" + Keys.Delete);
                emailInput.SendKeys("wanda@prueba.com");

                passwordInput.SendKeys(Keys.Control + "a" + Keys.Delete);
                passwordInput.SendKeys("MiPasswordSeguro123");

                submitButton.Click();

                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.Url.Contains("dashboard.html"));

                Assert.Contains("dashboard.html", _driver.Url);
            });
        }

        [Fact]
        public void Login_PruebaNegativa_ContrasenaIncorrecta_MuestraError()
        {
            EjecutarTest("Login_PruebaNegativa_ContrasenaIncorrecta_MuestraError", () =>
            {
                _driver.Navigate().GoToUrl(_baseUrl + "/index.html");

                var emailInput = _driver.FindElement(By.Id("email"));
                var passwordInput = _driver.FindElement(By.Id("password"));
                var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));

                emailInput.SendKeys(Keys.Control + "a" + Keys.Delete);
                emailInput.SendKeys("wanda@prueba.com");

                passwordInput.SendKeys(Keys.Control + "a" + Keys.Delete);
                passwordInput.SendKeys("ClaveErronea99");

                submitButton.Click();

                var errorAlert = _driver.FindElement(By.Id("errorAlert"));
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(d => !errorAlert.GetAttribute("class").Contains("d-none"));

                Assert.True(errorAlert.Displayed);
                Assert.Contains("Credenciales inválidas", errorAlert.Text);
                Assert.Contains("index.html", _driver.Url);
            });
        }

        [Fact]
        public void Login_PruebaLimites_CorreoInvalido_PrevieneEnvio()
        {
            EjecutarTest("Login_PruebaLimites_CorreoInvalido_PrevieneEnvio", () =>
            {
                _driver.Navigate().GoToUrl(_baseUrl + "/index.html");

                var emailInput = _driver.FindElement(By.Id("email"));
                var passwordInput = _driver.FindElement(By.Id("password"));
                var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));

                emailInput.SendKeys(Keys.Control + "a" + Keys.Delete);
                emailInput.SendKeys("wanda.pruebadominio.com");

                passwordInput.SendKeys(Keys.Control + "a" + Keys.Delete);
                passwordInput.SendKeys("");

                submitButton.Click();

                Assert.Contains("index.html", _driver.Url);
            });
        }

        [Fact]
        public void Gasto_CaminoFeliz_RegistroExitoso()
        {
            EjecutarTest("Gasto_CaminoFeliz_RegistroExitoso", () =>
            {
                RealizarLoginDePrueba();

                var floatButton = _driver.FindElement(By.CssSelector(".btn-float"));
                floatButton.Click();

                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.Id("modalGasto")).Displayed);

                var catSelect = _driver.FindElement(By.Id("selCategoria"));
                wait.Until(d => catSelect.FindElements(By.TagName("option")).Count > 0 &&
                                catSelect.FindElements(By.TagName("option"))[0].Text != "Cargando...");

                var metodoSelect = _driver.FindElement(By.Id("selMetodo"));
                wait.Until(d => metodoSelect.FindElements(By.TagName("option")).Count > 0 &&
                                metodoSelect.FindElements(By.TagName("option"))[0].Text != "Cargando...");

                var descInput = _driver.FindElement(By.Id("txtDescripcion"));
                var montoInput = _driver.FindElement(By.Id("txtMonto"));
                var saveButton = _driver.FindElement(By.CssSelector("#formGasto button[type='submit']"));

                descInput.Clear();
                descInput.SendKeys("Compra de comida por Selenium");
                montoInput.Clear();
                montoInput.SendKeys("350.00");

                saveButton.Click();

                var alert = wait.Until(d => {
                    try {
                        return d.SwitchTo().Alert();
                    }
                    catch (NoAlertPresentException) {
                        return null;
                    }
                });
                alert.Accept();

                wait.Until(d => d.FindElement(By.Id("tablaGastos")).Displayed);
                var tablaHtml = _driver.FindElement(By.Id("tablaGastos")).Text;

                Assert.Contains("Compra de comida por Selenium", tablaHtml);
            });
        }

        [Fact]
        public void Gasto_PruebaNegativa_MontoNegativo_Bloqueado()
        {
            EjecutarTest("Gasto_PruebaNegativa_MontoNegativo_Bloqueado", () =>
            {
                RealizarLoginDePrueba();

                var floatButton = _driver.FindElement(By.CssSelector(".btn-float"));
                floatButton.Click();

                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.Id("modalGasto")).Displayed);

                var catSelect = _driver.FindElement(By.Id("selCategoria"));
                wait.Until(d => catSelect.FindElements(By.TagName("option")).Count > 0 &&
                                catSelect.FindElements(By.TagName("option"))[0].Text != "Cargando...");

                var metodoSelect = _driver.FindElement(By.Id("selMetodo"));
                wait.Until(d => metodoSelect.FindElements(By.TagName("option")).Count > 0 &&
                                metodoSelect.FindElements(By.TagName("option"))[0].Text != "Cargando...");

                var descInput = _driver.FindElement(By.Id("txtDescripcion"));
                var montoInput = _driver.FindElement(By.Id("txtMonto"));
                var saveButton = _driver.FindElement(By.CssSelector("#formGasto button[type='submit']"));

                descInput.Clear();
                descInput.SendKeys("Gasto con monto inválido");
                montoInput.Clear();
                montoInput.SendKeys("-150.00");

                saveButton.Click();

                var alert = wait.Until(d => {
                    try {
                        return d.SwitchTo().Alert();
                    }
                    catch (NoAlertPresentException) {
                        return null;
                    }
                });
                alert.Accept();

                Assert.True(_driver.FindElement(By.Id("modalGasto")).Displayed);
            });
        }

        [Fact]
        public void Gasto_PruebaLimites_DescripcionLimiteCaracteres_GuardadoExitoso()
        {
            EjecutarTest("Gasto_PruebaLimites_DescripcionLimiteCaracteres_GuardadoExitoso", () =>
            {
                RealizarLoginDePrueba();

                var floatButton = _driver.FindElement(By.CssSelector(".btn-float"));
                floatButton.Click();

                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
                wait.Until(d => d.FindElement(By.Id("modalGasto")).Displayed);

                var catSelect = _driver.FindElement(By.Id("selCategoria"));
                wait.Until(d => catSelect.FindElements(By.TagName("option")).Count > 0 &&
                                catSelect.FindElements(By.TagName("option"))[0].Text != "Cargando...");

                var metodoSelect = _driver.FindElement(By.Id("selMetodo"));
                wait.Until(d => metodoSelect.FindElements(By.TagName("option")).Count > 0 &&
                                metodoSelect.FindElements(By.TagName("option"))[0].Text != "Cargando...");

                var descInput = _driver.FindElement(By.Id("txtDescripcion"));
                var montoInput = _driver.FindElement(By.Id("txtMonto"));
                var saveButton = _driver.FindElement(By.CssSelector("#formGasto button[type='submit']"));

                string descLimite = new string('X', 200);

                descInput.Clear();
                descInput.SendKeys(descLimite);
                montoInput.Clear();
                montoInput.SendKeys("100.00");

                saveButton.Click();

                var alert = wait.Until(d => {
                    try {
                        return d.SwitchTo().Alert();
                    }
                    catch (NoAlertPresentException) {
                        return null;
                    }
                });
                alert.Accept();

                wait.Until(d => d.FindElement(By.Id("tablaGastos")).Displayed);
                var tablaHtml = _driver.FindElement(By.Id("tablaGastos")).Text;

                Assert.Contains(descLimite, tablaHtml);
            });
        }

        private void RealizarLoginDePrueba()
        {
            _driver.Navigate().GoToUrl(_baseUrl + "/index.html");

            var emailInput = _driver.FindElement(By.Id("email"));
            emailInput.SendKeys(Keys.Control + "a" + Keys.Delete);
            emailInput.SendKeys("wanda@prueba.com");

            var passwordInput = _driver.FindElement(By.Id("password"));
            passwordInput.SendKeys(Keys.Control + "a" + Keys.Delete);
            passwordInput.SendKeys("MiPasswordSeguro123");

            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.Url.Contains("dashboard.html"));
        }
    }
}
