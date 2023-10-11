using Extracao_dados_Futebol.Constants;
using Extracao_dados_Futebol.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using static System.Net.WebRequestMethods;
using Extracao_dados_Futebol.Models;
using System;

namespace Extracao_dados_Futebol.Services
{
    public class Processamento : IProcessamento
    {
        public void Inicio()
        {
            using (IWebDriver driver = novoBrowser())
            {
                //maximizar(driver);
                string url = "https://www.ea.com/games/ea-sports-fc/ratings";
                navegar(driver, url);
                Thread.Sleep(1000);

                string local = "div.spacing_tokens__e5Cfc.fcTheme_tokens__PQ8qR section.Section_section__KHQne.dropMargins_margins___ATA0.Section_compact__9TZJq:nth-child(6) div.Pagination_paginationStyles__diVLn.Pagination_greaterThanFive__qd2wg div.Pagination_pageContainerStyles__dTV2N:nth-child(2) div.Pagination_buttonContainer__tCXJx:nth-child(11) > a.ButtonBase_outer__utJdC.ButtonBase_outlineVariant__M0jgK.ButtonBase_motion__UGbzl.Button_button__qYgy2.Button_mdSize__AZgW0.Button_fluidLayout__6_amc.Pagination_paginationButton__lTsAR";
                int Paginas = int.Parse(driver.FindElement(By.CssSelector(local)).Text);
                List<FCStats> ListaJogadores = new List<FCStats>();

                for (int i = 0; i < Paginas + 1; i++)
                {
                    string elementoID = "Table_rowCollapsibleCell__t3Ovl";
                    var lista = driver.FindElements(By.ClassName(elementoID));
                    int total = lista.Count * 2 + 1;
                    for (int j = 1; j < lista.Count; j = j + 2)
                    {
                        #region dados
                        string strRank = $@"//body/div[@id='__next']/div[@class='spacing_tokens__e5Cfc fcTheme_tokens__PQ8qR']/div[@style='background-color:var(--drop-color-neutral-1000)']/section[@class='Section_section__KHQne dropMargins_margins___ATA0 Section_compact__9TZJq']/table[@class='Table_table__C0OxY']/tbody[@class='Table_tbody__gYqSw']/tr[{j}]/td[1]/div[1]/div[1]";
                        string strPlayerImg = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[2]/div[1]/div[1]/div[1]/div[2]/div[1]/picture[1]/img[1]";
                        string strName = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[2]/div[1]/div[1]/div[2]/a[1]";
                        string strPais = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[3]/a[1]/div[1]/picture[1]/img[1]";
                        string strClube = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[4]/a[1]/div[1]/picture[1]/img[1]";
                        string strPosicao = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[5]/a[1]/span[1]";
                        string strOverall = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[6]/div[1]/div[1]/div[1]";
                        string strPace = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[7]/div[1]/div[1]/div[1]";
                        string strShooting = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[8]/div[1]/div[1]/div[1]";
                        string strPassing = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[9]/div[1]/div[1]/div[1]";
                        string strDribbling = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[10]/div[1]/div[1]/div[1]";
                        string strDefending = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[11]/div[1]/div[1]/div[1]";
                        string strPhysicality = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[12]/div[1]/div[1]/div[1]";

                        string strPePreferido = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/span[1]";
                        string strIdade = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[4]/span[1]";
                        string strAttWorkRate = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[5]";
                        string strDefWorkRate = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[7]";

                        string StrWeakFoot = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]";
                        string strSkillMoves = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[3]";
                        #endregion
                        var btnInfoAbre = $@"//html/body/div/div[2]/div/section[1]/table/tbody/tr[{j}]/td[13]/button";
                        //html/body/div/div[2]/div/section[1]/table/tbody/tr[101]/td[13]/button



                        int ctt = (j / 2) < 0 ? 1 : (j / 2);
                        var cliqueAbre = lista[ctt].FindElement(By.XPath(btnInfoAbre));
                        cliqueAbre.Click();

                        int ContadorWeekFoot = 0;
                        var a = (lista[(ctt)].FindElements(By.XPath(StrWeakFoot)));
                        for (int ae = 0; ae < a.Count; ae++)
                        {
                            var b = a[ae].FindElements(By.TagName("svg"));
                            foreach (var item in b)
                            {
                                var c = item.FindElement(By.TagName("path"));
                                var d = c.GetAttribute("d");
                                if (d.Contains("M5.14307"))
                                {
                                    ContadorWeekFoot++;
                                }
                                else if (d.Contains("M5.28564"))
                                {

                                }
                            }
                        }

                        int ContadorSkillMoves = 0;
                        var e = (lista[(ctt)].FindElements(By.XPath(strSkillMoves)));
                        for (int ae = 0; ae < a.Count; ae++)
                        {
                            var f = e[ae].FindElements(By.TagName("svg"));
                            foreach (var item in f)
                            {
                                var c = item.FindElement(By.TagName("path"));
                                var d = c.GetAttribute("d");
                                if (d.Contains("M5.14307"))
                                {
                                    ContadorSkillMoves++;
                                }
                                else if (d.Contains("M5.28564"))
                                {

                                }
                            }
                        }

                        string StrStylesPlus = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]";
                        List<FCStyles> fCStylesPlus = new List<FCStyles>();
                        var StylesPlus = (lista[(ctt)].FindElements(By.XPath(StrStylesPlus)));
                        foreach (var style in StylesPlus)
                        {
                            string sub = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]";
                            var sa = style.FindElement(By.XPath(sub)).FindElement(By.TagName("span")).Text;
                            var sb = style.FindElement(By.XPath(sub)).FindElement(By.TagName("picture")).FindElement(By.TagName("img")).GetAttribute("src");
                            fCStylesPlus.Add(
                                new FCStyles()
                                {
                                    StyleName = sa,
                                    StyleURLImg = sb
                                }
                            );
                        }

                        string StrStyles = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]";
                        List<FCStyles> fCStyles = new List<FCStyles>();
                        var Styles = (lista[(ctt)].FindElements(By.XPath(StrStyles)));
                        foreach (var style in Styles)
                        {
                            string listar = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]";
                            var count = lista[(ctt)].FindElements(By.XPath(listar));
                            var count2 = count[0].FindElements(By.TagName("div")).Count;

                            for (int iLinhas = 1; iLinhas < count2 + 1; iLinhas++)
                            {
                                string sub = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[{iLinhas}]";
                                var sa = style.FindElement(By.XPath(sub)).FindElement(By.TagName("span")).Text;
                                var sb = style.FindElement(By.XPath(sub)).FindElement(By.TagName("picture")).FindElement(By.TagName("img")).GetAttribute("src");
                                fCStyles.Add(
                                    new FCStyles()
                                    {
                                        StyleName = sa,
                                        StyleURLImg = sb
                                    }
                                );
                            }
                        }

                        #region Xpath Stats
                        string strPaceAcceleration = string.Empty;
                        string strPaceSprintSpeed = string.Empty;
                        string strShootPositioning = string.Empty;
                        string strShootFinishing = string.Empty;
                        string strShootShotPower = string.Empty;
                        string strShootLongShots = string.Empty;
                        string strShootVolleys = string.Empty;
                        string strShootPenalties = string.Empty;
                        string strPassingVision = string.Empty;
                        string strPassingCrossing = string.Empty;
                        string strPassingFreeKickAccuracy = string.Empty;
                        string strPassingShotPassing = string.Empty;
                        string strPassingLongPassing = string.Empty;
                        string strPassingCurve = string.Empty;
                        string strDribblingAgility = string.Empty;
                        string strDribblingBalance = string.Empty;
                        string strDribblingReactions = string.Empty;
                        string strDribblingBalControl = string.Empty;
                        string strDribblingDribbling = string.Empty;
                        string strDribblingComposure = string.Empty;
                        string strDefendingInterceptions = string.Empty;
                        string strDefendingHeadingAccuracy = string.Empty;
                        string strDefendingDefAwareness = string.Empty;
                        string strDefendingStandingTackle = string.Empty;
                        string strDefendingSlidingTackle = string.Empty;
                        string strPhysicalityJumping = string.Empty;
                        string strPhysicalityStamina = string.Empty;
                        string strPhysicalityStrength = string.Empty;
                        string strPhysicalityAggression = string.Empty;
                        string strGoalkeepingDiving = string.Empty;
                        string strGoalkeepingHandling = string.Empty;
                        string strGoalkeepingKicking = string.Empty;
                        string strGoalkeepingPositioning = string.Empty;
                        string strGoalkeepingReflexes = string.Empty;

                        #endregion

                        int PlayerRank = int.Parse(lista[j].FindElement(By.XPath(strRank)).Text);
                        string? PlayerName = lista[j].FindElement(By.XPath(strName)).Text;
                        string? PlayerNationality = lista[j].FindElement(By.XPath(strPais)).GetAttribute("alt");
                        string? PlayerNationalityFlagUrl = lista[j].FindElement(By.XPath(strPais)).GetAttribute("src");
                        string? PlayerPhotoURL = lista[j].FindElement(By.XPath(strPlayerImg)).GetAttribute("src");
                        string? PlayerClub = lista[j].FindElement(By.XPath(strClube)).GetAttribute("alt");
                        string? PlayerClubFlagUrl = lista[j].FindElement(By.XPath(strClube)).GetAttribute("src");
                        string? PlayerPosition = lista[j].FindElement(By.XPath(strPosicao)).Text;
                        //overall e outros statu;
                        int PlayerOverall = int.Parse(lista[j].FindElement(By.XPath(strOverall)).Text.Split("\r")[0]);
                        int PlayerPace = int.Parse(lista[j].FindElement(By.XPath(strPace)).Text.Split("\r")[0]);
                        int PlayerShooting = int.Parse(lista[j].FindElement(By.XPath(strShooting)).Text.Split("\r")[0]);
                        int PlayerPassing = int.Parse(lista[j].FindElement(By.XPath(strPassing)).Text.Split("\r")[0]);
                        int PlayerDribbling = int.Parse(lista[j].FindElement(By.XPath(strDribbling)).Text.Split("\r")[0]);
                        int PlayerDefending = int.Parse(lista[j].FindElement(By.XPath(strDefending)).Text.Split("\r")[0]);
                        int PlayerPhysicality = int.Parse(lista[j].FindElement(By.XPath(strPhysicality)).Text.Split("\r")[0]);
                        //linha ;
                        string? PlayerPreferredFoot = lista[j].FindElement(By.XPath(strPePreferido)).Text;
                        string? PlayerAge = lista[j].FindElement(By.XPath(strIdade)).Text;
                        string? PlayerAttWorkRate = lista[j].FindElement(By.XPath(strAttWorkRate)).Text.Split("\n")[1];
                        string? PlayerDefWorkRate = lista[j].FindElement(By.XPath(strDefWorkRate)).Text.Split("\n")[1];



                        if (PlayerPosition == "GK")
                        {
                            #region Goalkeeping
                            strGoalkeepingDiving = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[1]/li[2]/div[1]";
                            strGoalkeepingHandling = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[1]/li[3]/div[1]";
                            strGoalkeepingKicking = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[1]/li[4]/div[1]";
                            strGoalkeepingPositioning = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[1]/li[5]/div[1]";
                            strGoalkeepingReflexes = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[1]/li[6]/div[1]";
                            #endregion
                            #region Pace
                            strPaceAcceleration = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[2]/li[2]/div[1]";
                            strPaceSprintSpeed = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[2]/li[3]/div[1]";
                            #endregion
                            #region Shoot
                            strShootPositioning = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[2]/div[1]";
                            strShootFinishing = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[3]/div[1]";
                            strShootShotPower = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[4]/div[1]";
                            strShootLongShots = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[5]/div[1]";
                            strShootVolleys = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[6]/div[1]";
                            strShootPenalties = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[7]/div[1]";
                            #endregion
                            #region Passing
                            strPassingVision = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[2]/div[1]";
                            strPassingCrossing = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[3]/div[1]";
                            strPassingFreeKickAccuracy = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[4]/div[1]";
                            strPassingShotPassing = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[5]/div[1]";
                            strPassingLongPassing = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[6]/div[1]";
                            strPassingCurve = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[7]/div[1]";
                            #endregion
                            #region Dribbling
                            strDribblingAgility = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[2]/div[1]";
                            strDribblingBalance = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[3]/div[1]";
                            strDribblingReactions = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[4]/div[1]";
                            strDribblingBalControl = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[5]/div[1]";
                            strDribblingDribbling = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[6]/div[1]";
                            strDribblingComposure = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[7]/div[1]";
                            #endregion
                            #region Defending
                            strDefendingInterceptions = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[6]/li[2]/div[1]";
                            strDefendingHeadingAccuracy = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[6]/li[3]/div[1]";
                            strDefendingDefAwareness = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[6]/li[4]/div[1]";
                            strDefendingStandingTackle = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[6]/li[5]/div[1]";
                            strDefendingSlidingTackle = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[6]/li[6]/div[1]";
                            #endregion
                            #region Physicality
                            strPhysicalityJumping = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[7]/li[2]/div[1]";
                            strPhysicalityStamina = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[7]/li[3]/div[1]";
                            strPhysicalityStrength = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[7]/li[4]/div[1]";
                            strPhysicalityAggression = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[7]/li[5]/div[1]";
                            #endregion
                        }
                        else
                        {
                            #region Pace
                            strPaceAcceleration = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[1]/li[2]/div[1]";
                            strPaceSprintSpeed = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[1]/li[3]/div[1]";
                            #endregion
                            #region Shoot
                            strShootPositioning = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[2]/li[2]/div[1]";
                            strShootFinishing = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[2]/li[3]/div[1]";
                            strShootShotPower = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[2]/li[4]/div[1]";
                            strShootLongShots = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[2]/li[5]/div[1]";
                            strShootVolleys = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[2]/li[6]/div[1]";
                            strShootPenalties = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[2]/li[7]/div[1]";
                            #endregion
                            #region Passing
                            strPassingVision = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[2]/div[1]";
                            strPassingCrossing = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[3]/div[1]";
                            strPassingFreeKickAccuracy = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[4]/div[1]";
                            strPassingShotPassing = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[5]/div[1]";
                            strPassingLongPassing = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[6]/div[1]";
                            strPassingCurve = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[3]/li[7]/div[1]";
                            #endregion
                            #region Dribbling
                            strDribblingAgility = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[2]/div[1]";
                            strDribblingBalance = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[3]/div[1]";
                            strDribblingReactions = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[4]/div[1]";
                            strDribblingBalControl = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[5]/div[1]";
                            strDribblingDribbling = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[6]/div[1]";
                            strDribblingComposure = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[4]/li[7]/div[1]";
                            #endregion
                            #region Defending
                            strDefendingInterceptions = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[2]/div[1]";
                            strDefendingHeadingAccuracy = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[3]/div[1]";
                            strDefendingDefAwareness = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[4]/div[1]";
                            strDefendingStandingTackle = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[5]/div[1]";
                            strDefendingSlidingTackle = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[5]/li[6]/div[1]";
                            #endregion
                            #region Physicality
                            strPhysicalityJumping = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[6]/li[2]/div[1]";
                            strPhysicalityStamina = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[6]/li[3]/div[1]";
                            strPhysicalityStrength = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[6]/li[4]/div[1]";
                            strPhysicalityAggression = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j + 1}]/td[1]/div[1]/div[1]/div[1]/div[3]/ul[6]/li[5]/div[1]";
                            #endregion
                        }

                        int GoalkeepingDiving = 0;
                        int GoalkeepingHandling = 0;
                        int GoalkeepingKicking = 0;
                        int GoalkeepingPositioning = 0;
                        int GoalkeepingReflexes = 0;
                        //var teste = lista[j].FindElement(By.XPath(strPaceAcceleration)).Text.Split("\n")[1];
                        int PaceAcceleration = int.Parse(lista[j].FindElement(By.XPath(strPaceAcceleration)).Text.Split("\n")[1]);
                        int PaceSprintSpeed = int.Parse(lista[j].FindElement(By.XPath(strPaceSprintSpeed)).Text.Split("\n")[1]);
                        int ShootPositioning = int.Parse(lista[j].FindElement(By.XPath(strShootPositioning)).Text.Split("\n")[1]);
                        int ShootFinishing = int.Parse(lista[j].FindElement(By.XPath(strShootFinishing)).Text.Split("\n")[1]);
                        int ShootShotPower = int.Parse(lista[j].FindElement(By.XPath(strShootShotPower)).Text.Split("\n")[1]);
                        int ShootLongShots = int.Parse(lista[j].FindElement(By.XPath(strShootLongShots)).Text.Split("\n")[1]);
                        int ShootVolleys = int.Parse(lista[j].FindElement(By.XPath(strShootVolleys)).Text.Split("\n")[1]);
                        int ShootPenalties = int.Parse(lista[j].FindElement(By.XPath(strShootPenalties)).Text.Split("\n")[1]);
                        int PassingVision = int.Parse(lista[j].FindElement(By.XPath(strPassingVision)).Text.Split("\n")[1]);
                        int PassingCrossing = int.Parse(lista[j].FindElement(By.XPath(strPassingCrossing)).Text.Split("\n")[1]);
                        int PassingFreeKickAccuracy = int.Parse(lista[j].FindElement(By.XPath(strPassingFreeKickAccuracy)).Text.Split("\n")[1]);
                        int PassingShotPassing = int.Parse(lista[j].FindElement(By.XPath(strPassingShotPassing)).Text.Split("\n")[1]);
                        int PassingLongPassing = int.Parse(lista[j].FindElement(By.XPath(strPassingLongPassing)).Text.Split("\n")[1]);
                        int PassingCurve = int.Parse(lista[j].FindElement(By.XPath(strPassingCurve)).Text.Split("\n")[1]);
                        int DribblingAgility = int.Parse(lista[j].FindElement(By.XPath(strDribblingAgility)).Text.Split("\n")[1]);
                        int DribblingBalance = int.Parse(lista[j].FindElement(By.XPath(strDribblingBalance)).Text.Split("\n")[1]);
                        int DribblingReactions = int.Parse(lista[j].FindElement(By.XPath(strDribblingReactions)).Text.Split("\n")[1]);
                        int DribblingBalControl = int.Parse(lista[j].FindElement(By.XPath(strDribblingBalControl)).Text.Split("\n")[1]);
                        int DribblingDribbling = int.Parse(lista[j].FindElement(By.XPath(strDribblingDribbling)).Text.Split("\n")[1]);
                        int DribblingComposure = int.Parse(lista[j].FindElement(By.XPath(strDribblingComposure)).Text.Split("\n")[1]);
                        int DefendingInterceptions = int.Parse(lista[j].FindElement(By.XPath(strDefendingInterceptions)).Text.Split("\n")[1]);
                        int DefendingHeadingAccuracy = int.Parse(lista[j].FindElement(By.XPath(strDefendingHeadingAccuracy)).Text.Split("\n")[1]);
                        int DefendingDefAwareness = int.Parse(lista[j].FindElement(By.XPath(strDefendingDefAwareness)).Text.Split("\n")[1]);
                        int DefendingStandingTackle = int.Parse(lista[j].FindElement(By.XPath(strDefendingStandingTackle)).Text.Split("\n")[1]);
                        int DefendingSlidingTackle = int.Parse(lista[j].FindElement(By.XPath(strDefendingSlidingTackle)).Text.Split("\n")[1]);
                        int PhysicalityJumping = int.Parse(lista[j].FindElement(By.XPath(strPhysicalityJumping)).Text.Split("\n")[1]);
                        int PhysicalityStamina = int.Parse(lista[j].FindElement(By.XPath(strPhysicalityStamina)).Text.Split("\n")[1]);
                        int PhysicalityStrength = int.Parse(lista[j].FindElement(By.XPath(strPhysicalityStrength)).Text.Split("\n")[1]);
                        int PhysicalityAggression = int.Parse(lista[j].FindElement(By.XPath(strPhysicalityAggression)).Text.Split("\n")[1]);
                        if (PlayerPosition == "GK")
                        {
                            GoalkeepingDiving = int.Parse(lista[j].FindElement(By.XPath(strGoalkeepingDiving)).Text.Split("\n")[1]);
                            GoalkeepingHandling = int.Parse(lista[j].FindElement(By.XPath(strGoalkeepingHandling)).Text.Split("\n")[1]);
                            GoalkeepingKicking = int.Parse(lista[j].FindElement(By.XPath(strGoalkeepingKicking)).Text.Split("\n")[1]);
                            GoalkeepingPositioning = int.Parse(lista[j].FindElement(By.XPath(strGoalkeepingPositioning)).Text.Split("\n")[1]);
                            GoalkeepingReflexes = int.Parse(lista[j].FindElement(By.XPath(strGoalkeepingReflexes)).Text.Split("\n")[1]);
                        }

                        FCStats stats = new FCStats()
                        {
                            PlayerRank = PlayerRank,
                            PlayerName = PlayerName,
                            PlayerNationality = PlayerNationality,
                            PlayerNationalityFlagUrl = PlayerNationalityFlagUrl,
                            PlayerPhotoURL = PlayerPhotoURL,
                            PlayerClub = PlayerClub,
                            PlayerClubFlagUrl = PlayerClubFlagUrl,
                            PlayerPosition = PlayerPosition,
                            //overall e outros status
                            PlayerOverall = PlayerOverall,
                            PlayerPace = PlayerPace,
                            PlayerShooting = PlayerShooting,
                            PlayerPassing = PlayerPassing,
                            PlayerDribbling = PlayerDribbling,
                            PlayerDefending = PlayerDefending,
                            PlayerPhysicality = PlayerPhysicality,
                            //linha 2
                            PlayerPreferredFoot = PlayerPreferredFoot,
                            PlayerAge = PlayerAge,
                            PlayerAttWorkRate = PlayerAttWorkRate,
                            PlayerDefWorkRate = PlayerDefWorkRate,
                            PlayerSkillMoves = ContadorSkillMoves,
                            PlayerWeakFoot = ContadorWeekFoot,
                            PlayStyles = fCStylesPlus,
                            PlayStylesPlus = fCStyles,
                            StatsPace = new FCStatsPace()
                            {
                                Acceleration = PaceAcceleration,
                                SprintSpeed = PaceSprintSpeed
                            },
                            StatsShooting = new FCStatsShooting()
                            {
                                Positioning = ShootPositioning,
                                Finishing = ShootFinishing,
                                ShotPower = ShootShotPower,
                                LongShots = ShootLongShots,
                                Volleys = ShootVolleys,
                                Penalties = ShootPenalties
                            },
                            StatsPassing = new FCStatsPassing()
                            {
                                Vision = PassingVision,
                                Crossing = PassingCrossing,
                                FreeKickAccuracy = PassingFreeKickAccuracy,
                                ShotPassing = PassingShotPassing,
                                LongPassing = PassingLongPassing,
                                Curve = PassingCurve
                            },
                            StatsDribbling = new FCStatsDribbling()
                            {
                                Agility = DribblingAgility,
                                Balance = DribblingBalance,
                                Reactions = DribblingReactions,
                                BallControl = DribblingBalControl,
                                Dribbling = DribblingDribbling,
                                Composure = DribblingComposure
                            },
                            StatsDefending = new FCStatsDefending()
                            {
                                DefAwareness = DefendingInterceptions,
                                HeadingAccuracy = DefendingHeadingAccuracy,
                                Interceptions = DefendingDefAwareness,
                                SlidingTackle = DefendingStandingTackle,
                                StandingTackle = DefendingSlidingTackle
                            },
                            statsPhysicality = new FCStatsPhysicality()
                            {
                                Jumping = PhysicalityJumping,
                                Stamina = PhysicalityStamina,
                                Strength = PhysicalityStrength,
                                Aggression = PhysicalityAggression,
                            },
                            statsGoalkeeping = new FCStatsGoalkeeping()
                            {
                                Diving = GoalkeepingDiving,
                                Handling = GoalkeepingHandling,
                                Kicking = GoalkeepingKicking,
                                Positioning = GoalkeepingPositioning,
                                Reflexes = GoalkeepingReflexes,
                            }
                        };
                        ListaJogadores.Add(stats);

                        var btnInfoFecha = $@"/html[1]/body[1]/div[1]/div[2]/div[1]/section[1]/table[1]/tbody[1]/tr[{j}]/td[13]/button[1]";
                        var cliqueFecha = lista[j].FindElement(By.XPath(btnInfoFecha));
                        cliqueFecha.Click();
                    }



                    elementoID = "//a[@class='ButtonBase_outer__utJdC ButtonBase_outlineVariant__M0jgK ButtonBase_motion__UGbzl IconButton_square__Zk_96 IconButton_sizemd__FssAF IconButton_circle__4eVcH']";
                    var ProximaPagina = driver.FindElement(By.XPath(elementoID));
                    ProximaPagina.Click();

                }

                closeBrowser(driver);
            }
        }


        private bool maximizar(IWebDriver driver, bool maximizar = false)
        {
            driver.Manage().Window.Position.Offset(0, 0);
            driver.Manage().Window.Maximize();
            return !maximizar;
        }
        private ChromeDriver novoBrowser()
        {
            Uri origin = new Uri($@"https://chromedriver.chromium.org/downloads");
            TimeSpan time = TimeSpan.FromSeconds(60);

            ChromeVersion driver = new ChromeVersion();
            ChromeOptionsInternal chromeOptions = new ChromeOptionsInternal(true);
            driver.procuraChromeDriver(origin, Util.Caminho);
            driver.ExtraiZip(Util.Caminho + "chromedriver_win32.zip", Util.Caminho);


            ChromeDriver startBrowser = new ChromeDriver(
                Util.Caminho,
                chromeOptions.Options(true),
                time);
            //startBrowser.CloseDevToolsSession();
            //startBrowser.ClearNetworkConditions();

            return startBrowser;
        }

        private bool closeBrowser(IWebDriver driver)
        {
            try
            {
                driver.Close();
                driver.Quit();
                driver.Dispose();
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return false;
            }
        }

        private bool navegar(IWebDriver driver, string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return false;
            }
        }
    }
}
