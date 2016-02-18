using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Pic = DocumentFormat.OpenXml.Drawing.Pictures;
using V = DocumentFormat.OpenXml.Vml;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using M = DocumentFormat.OpenXml.Math;

using System.Web;
using System.Configuration;

namespace SEMP.Data
{//luis - 2011-12-19

    public abstract class SolicitudePrint
    {
        const string s00F57926 = "00F57926";

        //logo pfizer
        //<< luis - 2012-08-03
        //private string imagePart1Data = "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";
        private string imagePart1Data = "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";
        //>>

        public abstract List<SolicitudeToPrintResult> ToList();

        public string Report()
        {
            var List = ToList();
            string sFile = DateTime.Now.ToString("yyMMddHHmmss") + DateTime.Now.Millisecond + "_SolicitudeToPrint.docx";
            string sPath = ConfigurationSettings.AppSettings["ReportPath"].ToString();
            string sMapPath_File = HttpContext.Current.Server.MapPath(sPath + "\\" + sFile);
            string sApplicationPath_File = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + sPath + "/" + sFile;
            CreatePackage(sMapPath_File, List);
            return sApplicationPath_File;
        }
        private void CreatePackage(String filePath, List<SolicitudeToPrintResult> List)
        {
            using (WordprocessingDocument package = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                CreateParts(package, List);
            }
        }
        private void CreateParts(WordprocessingDocument document, List<SolicitudeToPrintResult> List)
        {
            ExtendedFilePropertiesPart extendedFilePropertiesPart1 = document.AddNewPart<ExtendedFilePropertiesPart>("rId3");
            extendedFilePropertiesPart1.Properties = new Ap.Properties();

            MainDocumentPart mainDocumentPart1 = document.AddMainDocumentPart();
            GenerateMainDocumentPart1Content(mainDocumentPart1, List);

            WebSettingsPart webSettingsPart1 = mainDocumentPart1.AddNewPart<WebSettingsPart>("rId3");
            webSettingsPart1.WebSettings = new WebSettings();

            DocumentSettingsPart documentSettingsPart1 = mainDocumentPart1.AddNewPart<DocumentSettingsPart>("rId2");
            documentSettingsPart1.Settings = new Settings();

            StyleDefinitionsPart styleDefinitionsPart1 = mainDocumentPart1.AddNewPart<StyleDefinitionsPart>("rId1");
            styleDefinitionsPart1.Styles = new Styles();

            ThemePart themePart1 = mainDocumentPart1.AddNewPart<ThemePart>("rId6");
            GenerateThemePart1Content(themePart1);

            FontTablePart fontTablePart1 = mainDocumentPart1.AddNewPart<FontTablePart>("rId5");
            GenerateFontTablePart1Content(fontTablePart1);

            //imagen del logo de pfizer
            ImagePart imagePart1 = mainDocumentPart1.AddNewPart<ImagePart>("image/jpeg", "rId4");
            GenerateImagePart1Content(imagePart1);

        }
        private void GenerateMainDocumentPart1Content(MainDocumentPart mainDocumentPart1, List<SolicitudeToPrintResult> List)
        {
            Body body1 = new Body();
            int nSolicitude = 0;
            int nSolicitudes = 0;
            int nDetails = 0;
            Table table = new Table();
            foreach (var x in List)
            {
                if (Convert.ToInt32(x.nSolicitude.ToString()) != nSolicitude)
                {
                    if (nSolicitude != 0)
                    {
                        for (int i = ++nDetails ; i <= 6; i++)
                        {
                            table.Append(TableRowNew_AddDetailItem("", "", "", ""));
                        }
                        body1.Append(table);
                        body1.Append(new Paragraph() { RsidParagraphAddition = "000A1AAF", RsidRunAdditionDefault = "000A1AAF" });
                        if (nSolicitudes >= 1)
                        {
                            body1.Append(ParagraphNew_BreakValuesPage());
                            nSolicitudes = 0;
                        }
                        else
                        {
                            nSolicitudes++;
                        }
                    }
                    nSolicitude = Convert.ToInt32(x.nSolicitude.ToString());
                    nDetails = 0;
                    //Neha- 21-oct-2013
                    body1.Append(TableNew_Head(x.sTypeSolicitor, nSolicitude.ToString(), ((DateTime)x.dtDate).ToString("dd/MM/yy"), x.sSolicitorFirstName, x.sSolicitorLastName, x.sDepartment, x.sSolicitudeType, (x.dtExpiration == null ? "" : ((DateTime)x.dtExpiration).ToShortDateString())));
                    //body1.Append(TableNew_Head(x.sTypeSolicitor, nSolicitude.ToString(), ((DateTime)x.dtDate).ToString("dd/MM/yy"), x.sSolicitorFirstName, x.sSolicitorLastName, x.sDepartment));
                    body1.Append(ParagraphNew_EndHead());
                    table = TableNew_Detail();
                }
                table.Append(TableRowNew_AddDetailItem(x.sProduct, x.sProductName, x.sObservation, x.nAmount.ToString()));
                nDetails++;
            }
            body1.Append(table);
            body1.Append(new Paragraph() { RsidParagraphAddition = "000A1AAF", RsidRunAdditionDefault = "000A1AAF" });
            body1.Append(SectionPropertiesNew());
            Document document1 = new Document();
            document1.Append(body1);
            mainDocumentPart1.Document = document1;
        }
        private void GenerateThemePart1Content(ThemePart themePart1)
        {
            A.Theme theme1 = new A.Theme() { Name = "Office Theme" };

            A.ThemeElements themeElements1 = new A.ThemeElements();

            A.ColorScheme colorScheme1 = new A.ColorScheme() { Name = "Office" };

            A.Dark1Color dark1Color1 = new A.Dark1Color();
            A.SystemColor systemColor1 = new A.SystemColor() { Val = A.SystemColorValues.WindowText, LastColor = "000000" };

            dark1Color1.Append(systemColor1);

            A.Light1Color light1Color1 = new A.Light1Color();
            A.SystemColor systemColor2 = new A.SystemColor() { Val = A.SystemColorValues.Window, LastColor = "FFFFFF" };

            light1Color1.Append(systemColor2);

            A.Dark2Color dark2Color1 = new A.Dark2Color();
            A.RgbColorModelHex rgbColorModelHex1 = new A.RgbColorModelHex() { Val = "1F497D" };

            dark2Color1.Append(rgbColorModelHex1);

            A.Light2Color light2Color1 = new A.Light2Color();
            A.RgbColorModelHex rgbColorModelHex2 = new A.RgbColorModelHex() { Val = "EEECE1" };

            light2Color1.Append(rgbColorModelHex2);

            A.Accent1Color accent1Color1 = new A.Accent1Color();
            A.RgbColorModelHex rgbColorModelHex3 = new A.RgbColorModelHex() { Val = "4F81BD" };

            accent1Color1.Append(rgbColorModelHex3);

            A.Accent2Color accent2Color1 = new A.Accent2Color();
            A.RgbColorModelHex rgbColorModelHex4 = new A.RgbColorModelHex() { Val = "C0504D" };

            accent2Color1.Append(rgbColorModelHex4);

            A.Accent3Color accent3Color1 = new A.Accent3Color();
            A.RgbColorModelHex rgbColorModelHex5 = new A.RgbColorModelHex() { Val = "9BBB59" };

            accent3Color1.Append(rgbColorModelHex5);

            A.Accent4Color accent4Color1 = new A.Accent4Color();
            A.RgbColorModelHex rgbColorModelHex6 = new A.RgbColorModelHex() { Val = "8064A2" };

            accent4Color1.Append(rgbColorModelHex6);

            A.Accent5Color accent5Color1 = new A.Accent5Color();
            A.RgbColorModelHex rgbColorModelHex7 = new A.RgbColorModelHex() { Val = "4BACC6" };

            accent5Color1.Append(rgbColorModelHex7);

            A.Accent6Color accent6Color1 = new A.Accent6Color();
            A.RgbColorModelHex rgbColorModelHex8 = new A.RgbColorModelHex() { Val = "F79646" };

            accent6Color1.Append(rgbColorModelHex8);

            A.Hyperlink hyperlink1 = new A.Hyperlink();
            A.RgbColorModelHex rgbColorModelHex9 = new A.RgbColorModelHex() { Val = "0000FF" };

            hyperlink1.Append(rgbColorModelHex9);

            A.FollowedHyperlinkColor followedHyperlinkColor1 = new A.FollowedHyperlinkColor();
            A.RgbColorModelHex rgbColorModelHex10 = new A.RgbColorModelHex() { Val = "800080" };

            followedHyperlinkColor1.Append(rgbColorModelHex10);

            colorScheme1.Append(dark1Color1);
            colorScheme1.Append(light1Color1);
            colorScheme1.Append(dark2Color1);
            colorScheme1.Append(light2Color1);
            colorScheme1.Append(accent1Color1);
            colorScheme1.Append(accent2Color1);
            colorScheme1.Append(accent3Color1);
            colorScheme1.Append(accent4Color1);
            colorScheme1.Append(accent5Color1);
            colorScheme1.Append(accent6Color1);
            colorScheme1.Append(hyperlink1);
            colorScheme1.Append(followedHyperlinkColor1);

            A.FontScheme fontScheme1 = new A.FontScheme() { Name = "Office" };

            A.MajorFont majorFont1 = new A.MajorFont();
            A.LatinFont latinFont1 = new A.LatinFont() { Typeface = "Cambria" };
            A.EastAsianFont eastAsianFont1 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont1 = new A.ComplexScriptFont() { Typeface = "" };

            majorFont1.Append(latinFont1);
            majorFont1.Append(eastAsianFont1);
            majorFont1.Append(complexScriptFont1);

            A.MinorFont minorFont1 = new A.MinorFont();
            A.LatinFont latinFont2 = new A.LatinFont() { Typeface = "Calibri" };
            A.EastAsianFont eastAsianFont2 = new A.EastAsianFont() { Typeface = "" };
            A.ComplexScriptFont complexScriptFont2 = new A.ComplexScriptFont() { Typeface = "" };

            minorFont1.Append(latinFont2);
            minorFont1.Append(eastAsianFont2);
            minorFont1.Append(complexScriptFont2);

            fontScheme1.Append(majorFont1);
            fontScheme1.Append(minorFont1);

            A.FormatScheme formatScheme1 = new A.FormatScheme() { Name = "Office" };

            A.FillStyleList fillStyleList1 = new A.FillStyleList();

            A.SolidFill solidFill1 = new A.SolidFill();
            A.SchemeColor schemeColor1 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill1.Append(schemeColor1);

            A.GradientFill gradientFill1 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList1 = new A.GradientStopList();

            A.GradientStop gradientStop1 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor2 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint1 = new A.Tint() { Val = 50000 };
            A.SaturationModulation saturationModulation1 = new A.SaturationModulation() { Val = 300000 };

            schemeColor2.Append(tint1);
            schemeColor2.Append(saturationModulation1);

            gradientStop1.Append(schemeColor2);

            A.GradientStop gradientStop2 = new A.GradientStop() { Position = 35000 };

            A.SchemeColor schemeColor3 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint2 = new A.Tint() { Val = 37000 };
            A.SaturationModulation saturationModulation2 = new A.SaturationModulation() { Val = 300000 };

            schemeColor3.Append(tint2);
            schemeColor3.Append(saturationModulation2);

            gradientStop2.Append(schemeColor3);

            A.GradientStop gradientStop3 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor4 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint3 = new A.Tint() { Val = 15000 };
            A.SaturationModulation saturationModulation3 = new A.SaturationModulation() { Val = 350000 };

            schemeColor4.Append(tint3);
            schemeColor4.Append(saturationModulation3);

            gradientStop3.Append(schemeColor4);

            gradientStopList1.Append(gradientStop1);
            gradientStopList1.Append(gradientStop2);
            gradientStopList1.Append(gradientStop3);
            A.LinearGradientFill linearGradientFill1 = new A.LinearGradientFill() { Angle = 16200000, Scaled = true };

            gradientFill1.Append(gradientStopList1);
            gradientFill1.Append(linearGradientFill1);

            A.GradientFill gradientFill2 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList2 = new A.GradientStopList();

            A.GradientStop gradientStop4 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor5 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade1 = new A.Shade() { Val = 51000 };
            A.SaturationModulation saturationModulation4 = new A.SaturationModulation() { Val = 130000 };

            schemeColor5.Append(shade1);
            schemeColor5.Append(saturationModulation4);

            gradientStop4.Append(schemeColor5);

            A.GradientStop gradientStop5 = new A.GradientStop() { Position = 80000 };

            A.SchemeColor schemeColor6 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade2 = new A.Shade() { Val = 93000 };
            A.SaturationModulation saturationModulation5 = new A.SaturationModulation() { Val = 130000 };

            schemeColor6.Append(shade2);
            schemeColor6.Append(saturationModulation5);

            gradientStop5.Append(schemeColor6);

            A.GradientStop gradientStop6 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor7 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade3 = new A.Shade() { Val = 94000 };
            A.SaturationModulation saturationModulation6 = new A.SaturationModulation() { Val = 135000 };

            schemeColor7.Append(shade3);
            schemeColor7.Append(saturationModulation6);

            gradientStop6.Append(schemeColor7);

            gradientStopList2.Append(gradientStop4);
            gradientStopList2.Append(gradientStop5);
            gradientStopList2.Append(gradientStop6);
            A.LinearGradientFill linearGradientFill2 = new A.LinearGradientFill() { Angle = 16200000, Scaled = false };

            gradientFill2.Append(gradientStopList2);
            gradientFill2.Append(linearGradientFill2);

            fillStyleList1.Append(solidFill1);
            fillStyleList1.Append(gradientFill1);
            fillStyleList1.Append(gradientFill2);

            A.LineStyleList lineStyleList1 = new A.LineStyleList();

            A.Outline outline2 = new A.Outline() { Width = 9525, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill2 = new A.SolidFill();

            A.SchemeColor schemeColor8 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade4 = new A.Shade() { Val = 95000 };
            A.SaturationModulation saturationModulation7 = new A.SaturationModulation() { Val = 105000 };

            schemeColor8.Append(shade4);
            schemeColor8.Append(saturationModulation7);

            solidFill2.Append(schemeColor8);
            A.PresetDash presetDash1 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline2.Append(solidFill2);
            outline2.Append(presetDash1);

            A.Outline outline3 = new A.Outline() { Width = 25400, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill3 = new A.SolidFill();
            A.SchemeColor schemeColor9 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill3.Append(schemeColor9);
            A.PresetDash presetDash2 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline3.Append(solidFill3);
            outline3.Append(presetDash2);

            A.Outline outline4 = new A.Outline() { Width = 38100, CapType = A.LineCapValues.Flat, CompoundLineType = A.CompoundLineValues.Single, Alignment = A.PenAlignmentValues.Center };

            A.SolidFill solidFill4 = new A.SolidFill();
            A.SchemeColor schemeColor10 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill4.Append(schemeColor10);
            A.PresetDash presetDash3 = new A.PresetDash() { Val = A.PresetLineDashValues.Solid };

            outline4.Append(solidFill4);
            outline4.Append(presetDash3);

            lineStyleList1.Append(outline2);
            lineStyleList1.Append(outline3);
            lineStyleList1.Append(outline4);

            A.EffectStyleList effectStyleList1 = new A.EffectStyleList();

            A.EffectStyle effectStyle1 = new A.EffectStyle();

            A.EffectList effectList1 = new A.EffectList();

            A.OuterShadow outerShadow1 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex11 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha1 = new A.Alpha() { Val = 38000 };

            rgbColorModelHex11.Append(alpha1);

            outerShadow1.Append(rgbColorModelHex11);

            effectList1.Append(outerShadow1);

            effectStyle1.Append(effectList1);

            A.EffectStyle effectStyle2 = new A.EffectStyle();

            A.EffectList effectList2 = new A.EffectList();

            A.OuterShadow outerShadow2 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex12 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha2 = new A.Alpha() { Val = 35000 };

            rgbColorModelHex12.Append(alpha2);

            outerShadow2.Append(rgbColorModelHex12);

            effectList2.Append(outerShadow2);

            effectStyle2.Append(effectList2);

            A.EffectStyle effectStyle3 = new A.EffectStyle();

            A.EffectList effectList3 = new A.EffectList();

            A.OuterShadow outerShadow3 = new A.OuterShadow() { BlurRadius = 40000L, Distance = 23000L, Direction = 5400000, RotateWithShape = false };

            A.RgbColorModelHex rgbColorModelHex13 = new A.RgbColorModelHex() { Val = "000000" };
            A.Alpha alpha3 = new A.Alpha() { Val = 35000 };

            rgbColorModelHex13.Append(alpha3);

            outerShadow3.Append(rgbColorModelHex13);

            effectList3.Append(outerShadow3);

            A.Scene3DType scene3DType1 = new A.Scene3DType();

            A.Camera camera1 = new A.Camera() { Preset = A.PresetCameraValues.OrthographicFront };
            A.Rotation rotation1 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 0 };

            camera1.Append(rotation1);

            A.LightRig lightRig1 = new A.LightRig() { Rig = A.LightRigValues.ThreePoints, Direction = A.LightRigDirectionValues.Top };
            A.Rotation rotation2 = new A.Rotation() { Latitude = 0, Longitude = 0, Revolution = 1200000 };

            lightRig1.Append(rotation2);

            scene3DType1.Append(camera1);
            scene3DType1.Append(lightRig1);

            A.Shape3DType shape3DType1 = new A.Shape3DType();
            A.BevelTop bevelTop1 = new A.BevelTop() { Width = 63500L, Height = 25400L };

            shape3DType1.Append(bevelTop1);

            effectStyle3.Append(effectList3);
            effectStyle3.Append(scene3DType1);
            effectStyle3.Append(shape3DType1);

            effectStyleList1.Append(effectStyle1);
            effectStyleList1.Append(effectStyle2);
            effectStyleList1.Append(effectStyle3);

            A.BackgroundFillStyleList backgroundFillStyleList1 = new A.BackgroundFillStyleList();

            A.SolidFill solidFill5 = new A.SolidFill();
            A.SchemeColor schemeColor11 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };

            solidFill5.Append(schemeColor11);

            A.GradientFill gradientFill3 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList3 = new A.GradientStopList();

            A.GradientStop gradientStop7 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor12 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint4 = new A.Tint() { Val = 40000 };
            A.SaturationModulation saturationModulation8 = new A.SaturationModulation() { Val = 350000 };

            schemeColor12.Append(tint4);
            schemeColor12.Append(saturationModulation8);

            gradientStop7.Append(schemeColor12);

            A.GradientStop gradientStop8 = new A.GradientStop() { Position = 40000 };

            A.SchemeColor schemeColor13 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint5 = new A.Tint() { Val = 45000 };
            A.Shade shade5 = new A.Shade() { Val = 99000 };
            A.SaturationModulation saturationModulation9 = new A.SaturationModulation() { Val = 350000 };

            schemeColor13.Append(tint5);
            schemeColor13.Append(shade5);
            schemeColor13.Append(saturationModulation9);

            gradientStop8.Append(schemeColor13);

            A.GradientStop gradientStop9 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor14 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade6 = new A.Shade() { Val = 20000 };
            A.SaturationModulation saturationModulation10 = new A.SaturationModulation() { Val = 255000 };

            schemeColor14.Append(shade6);
            schemeColor14.Append(saturationModulation10);

            gradientStop9.Append(schemeColor14);

            gradientStopList3.Append(gradientStop7);
            gradientStopList3.Append(gradientStop8);
            gradientStopList3.Append(gradientStop9);

            A.PathGradientFill pathGradientFill1 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle1 = new A.FillToRectangle() { Left = 50000, Top = -80000, Right = 50000, Bottom = 180000 };

            pathGradientFill1.Append(fillToRectangle1);

            gradientFill3.Append(gradientStopList3);
            gradientFill3.Append(pathGradientFill1);

            A.GradientFill gradientFill4 = new A.GradientFill() { RotateWithShape = true };

            A.GradientStopList gradientStopList4 = new A.GradientStopList();

            A.GradientStop gradientStop10 = new A.GradientStop() { Position = 0 };

            A.SchemeColor schemeColor15 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Tint tint6 = new A.Tint() { Val = 80000 };
            A.SaturationModulation saturationModulation11 = new A.SaturationModulation() { Val = 300000 };

            schemeColor15.Append(tint6);
            schemeColor15.Append(saturationModulation11);

            gradientStop10.Append(schemeColor15);

            A.GradientStop gradientStop11 = new A.GradientStop() { Position = 100000 };

            A.SchemeColor schemeColor16 = new A.SchemeColor() { Val = A.SchemeColorValues.PhColor };
            A.Shade shade7 = new A.Shade() { Val = 30000 };
            A.SaturationModulation saturationModulation12 = new A.SaturationModulation() { Val = 200000 };

            schemeColor16.Append(shade7);
            schemeColor16.Append(saturationModulation12);

            gradientStop11.Append(schemeColor16);

            gradientStopList4.Append(gradientStop10);
            gradientStopList4.Append(gradientStop11);

            A.PathGradientFill pathGradientFill2 = new A.PathGradientFill() { Path = A.PathShadeValues.Circle };
            A.FillToRectangle fillToRectangle2 = new A.FillToRectangle() { Left = 50000, Top = 50000, Right = 50000, Bottom = 50000 };

            pathGradientFill2.Append(fillToRectangle2);

            gradientFill4.Append(gradientStopList4);
            gradientFill4.Append(pathGradientFill2);

            backgroundFillStyleList1.Append(solidFill5);
            backgroundFillStyleList1.Append(gradientFill3);
            backgroundFillStyleList1.Append(gradientFill4);

            formatScheme1.Append(fillStyleList1);
            formatScheme1.Append(lineStyleList1);
            formatScheme1.Append(effectStyleList1);
            formatScheme1.Append(backgroundFillStyleList1);

            themeElements1.Append(colorScheme1);
            themeElements1.Append(fontScheme1);
            themeElements1.Append(formatScheme1);
            A.ObjectDefaults objectDefaults1 = new A.ObjectDefaults();
            A.ExtraColorSchemeList extraColorSchemeList1 = new A.ExtraColorSchemeList();


            theme1.Append(themeElements1);
            theme1.Append(objectDefaults1);
            theme1.Append(extraColorSchemeList1);
            themePart1.Theme = theme1;
        }
        private void GenerateFontTablePart1Content(FontTablePart fontTablePart1)
        {
            Fonts fonts1 = new Fonts();

            Font font1 = new Font() { Name = "Calibri" };
            Panose1Number panose1Number1 = new Panose1Number() { Val = "020F0502020204030204" };
            FontCharSet fontCharSet1 = new FontCharSet() { Val = "00" };
            FontFamily fontFamily1 = new FontFamily() { Val = FontFamilyValues.Swiss };
            Pitch pitch1 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature1 = new FontSignature() { UnicodeSignature0 = "A00002EF", UnicodeSignature1 = "4000207B", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000009F", CodePageSignature1 = "00000000" };

            font1.Append(panose1Number1);
            font1.Append(fontCharSet1);
            font1.Append(fontFamily1);
            font1.Append(pitch1);
            font1.Append(fontSignature1);

            Font font2 = new Font() { Name = "Times New Roman" };
            Panose1Number panose1Number2 = new Panose1Number() { Val = "02020603050405020304" };
            FontCharSet fontCharSet2 = new FontCharSet() { Val = "00" };
            FontFamily fontFamily2 = new FontFamily() { Val = FontFamilyValues.Roman };
            Pitch pitch2 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature2 = new FontSignature() { UnicodeSignature0 = "20002A87", UnicodeSignature1 = "80000000", UnicodeSignature2 = "00000008", UnicodeSignature3 = "00000000", CodePageSignature0 = "000001FF", CodePageSignature1 = "00000000" };

            font2.Append(panose1Number2);
            font2.Append(fontCharSet2);
            font2.Append(fontFamily2);
            font2.Append(pitch2);
            font2.Append(fontSignature2);

            Font font3 = new Font() { Name = "Tahoma" };
            Panose1Number panose1Number3 = new Panose1Number() { Val = "020B0604030504040204" };
            FontCharSet fontCharSet3 = new FontCharSet() { Val = "00" };
            FontFamily fontFamily3 = new FontFamily() { Val = FontFamilyValues.Swiss };
            NotTrueType notTrueType1 = new NotTrueType();
            Pitch pitch3 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature3 = new FontSignature() { UnicodeSignature0 = "00000003", UnicodeSignature1 = "00000000", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "00000001", CodePageSignature1 = "00000000" };

            font3.Append(panose1Number3);
            font3.Append(fontCharSet3);
            font3.Append(fontFamily3);
            font3.Append(notTrueType1);
            font3.Append(pitch3);
            font3.Append(fontSignature3);

            Font font4 = new Font() { Name = "Verdana" };
            Panose1Number panose1Number4 = new Panose1Number() { Val = "020B0604030504040204" };
            FontCharSet fontCharSet4 = new FontCharSet() { Val = "00" };
            FontFamily fontFamily4 = new FontFamily() { Val = FontFamilyValues.Swiss };
            Pitch pitch4 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature4 = new FontSignature() { UnicodeSignature0 = "20000287", UnicodeSignature1 = "00000000", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000019F", CodePageSignature1 = "00000000" };

            font4.Append(panose1Number4);
            font4.Append(fontCharSet4);
            font4.Append(fontFamily4);
            font4.Append(pitch4);
            font4.Append(fontSignature4);

            Font font5 = new Font() { Name = "Cambria" };
            Panose1Number panose1Number5 = new Panose1Number() { Val = "02040503050406030204" };
            FontCharSet fontCharSet5 = new FontCharSet() { Val = "00" };
            FontFamily fontFamily5 = new FontFamily() { Val = FontFamilyValues.Roman };
            Pitch pitch5 = new Pitch() { Val = FontPitchValues.Variable };
            FontSignature fontSignature5 = new FontSignature() { UnicodeSignature0 = "A00002EF", UnicodeSignature1 = "4000004B", UnicodeSignature2 = "00000000", UnicodeSignature3 = "00000000", CodePageSignature0 = "0000009F", CodePageSignature1 = "00000000" };

            font5.Append(panose1Number5);
            font5.Append(fontCharSet5);
            font5.Append(fontFamily5);
            font5.Append(pitch5);
            font5.Append(fontSignature5);

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);
            fonts1.Append(font4);
            fonts1.Append(font5);

            fontTablePart1.Fonts = fonts1;
        }
        private void GenerateImagePart1Content(ImagePart imagePart1)
        {//logo pfizer
            System.IO.Stream data = GetBinaryDataStream(imagePart1Data);
            imagePart1.FeedData(data);
            data.Close();
        }
        
        private System.IO.Stream GetBinaryDataStream(string base64String)
        {
            return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
        }
        
        private Run RunNew(string sText, string sFontSizeVal, bool bBold, bool bNoProof, bool bColor)
        {
            Run run1 = new Run() { RsidRunProperties = s00F57926 };
            run1.Append(RunPropertiesNew(sFontSizeVal, bBold, bNoProof, bColor));
            run1.Append(new Text() { Text = sText });
            return run1;
        }
        private Run RunNew(string sFontSizeVal, bool bBold, bool bNoProof, bool bColor, Drawing drawing1)
        {
            Run run1 = new Run();
            run1.Append(RunPropertiesNew(sFontSizeVal, bBold, bNoProof, bColor));
            run1.Append(drawing1);
            return run1;
        }
        private Run RunNew(string sFontSizeVal, bool bBold, bool bNoProof, bool bColor, Picture picture2)
        {
            Run run1 = new Run() { RsidRunProperties = "00BB454C" };
            run1.Append(RunPropertiesNew(sFontSizeVal, bBold, bNoProof, bColor));
            run1.Append(picture2);
            return run1;
        }
        private ParagraphProperties ParagraphPropertiesNew(JustificationValues nJustificationValuesValue, string sFontSizeComplexScriptVal, bool bBold, bool bVanish)
        {
            ParagraphProperties paragraphProperties1 = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };
            Justification justification1 = new Justification() { Val = nJustificationValuesValue };

            ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
            RunFonts runFonts1 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" };

            FontSize fontSize1 = new FontSize() { Val = sFontSizeComplexScriptVal };
            FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = sFontSizeComplexScriptVal };
            Languages languages1 = new Languages() { EastAsia = "es-AR" };

            paragraphMarkRunProperties1.Append(runFonts1);
            if (bBold)
            {
                Bold bold1 = new Bold();
                BoldComplexScript boldComplexScript1 = new BoldComplexScript();
                Color color1 = new Color() { Val = "000000" };
                paragraphMarkRunProperties1.Append(bold1);
                paragraphMarkRunProperties1.Append(boldComplexScript1);
                paragraphMarkRunProperties1.Append(color1);
            }
            if (bVanish)
            {
                paragraphMarkRunProperties1.Append(new Vanish());
            }
            paragraphMarkRunProperties1.Append(fontSize1);
            paragraphMarkRunProperties1.Append(fontSizeComplexScript1);
            paragraphMarkRunProperties1.Append(languages1);

            paragraphProperties1.Append(spacingBetweenLines1);
            paragraphProperties1.Append(justification1);
            paragraphProperties1.Append(paragraphMarkRunProperties1);
            return paragraphProperties1;
        }
        private RunProperties RunPropertiesNew(string sFontSizeVal, bool bBold, bool bNoProof, bool bColor)
        {
            RunProperties runProperties2 = new RunProperties();
            runProperties2.Append(new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Times New Roman", ComplexScript = "Times New Roman" });
            if (bBold) runProperties2.Append(new Bold());
            if (bNoProof) runProperties2.Append(new NoProof());
            runProperties2.Append(new BoldComplexScript());
            if (bColor) runProperties2.Append(new Color() { Val = "000000" });
            runProperties2.Append(new FontSize() { Val = sFontSizeVal });
            runProperties2.Append(new FontSizeComplexScript() { Val = sFontSizeVal });
            runProperties2.Append(new Languages() { EastAsia = "es-AR" });
            return runProperties2;
        }
        private Drawing DrawingNew()
        {
            Drawing drawing1 = new Drawing();

            Wp.Inline inline1 = new Wp.Inline() { DistanceFromTop = (UInt32Value)0U, DistanceFromBottom = (UInt32Value)0U, DistanceFromLeft = (UInt32Value)0U, DistanceFromRight = (UInt32Value)0U };
            Wp.Extent extent1 = new Wp.Extent() { Cx = 809625L, Cy = 485775L };
            Wp.EffectExtent effectExtent1 = new Wp.EffectExtent() { LeftEdge = 19050L, TopEdge = 0L, RightEdge = 9525L, BottomEdge = 0L };
            Wp.DocProperties docProperties1 = new Wp.DocProperties() { Id = (UInt32Value)1U, Name = "Picture 1", Description = "http://bueamrweb03/smp/images/blue.sm.jpg" };

            Wp.NonVisualGraphicFrameDrawingProperties nonVisualGraphicFrameDrawingProperties1 = new Wp.NonVisualGraphicFrameDrawingProperties();

            A.GraphicFrameLocks graphicFrameLocks1 = new A.GraphicFrameLocks() { NoChangeAspect = true };

            nonVisualGraphicFrameDrawingProperties1.Append(graphicFrameLocks1);

            A.Graphic graphic1 = new A.Graphic();

            A.GraphicData graphicData1 = new A.GraphicData() { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" };

            Pic.Picture picture1 = new Pic.Picture();

            Pic.NonVisualPictureProperties nonVisualPictureProperties1 = new Pic.NonVisualPictureProperties();

            Pic.NonVisualPictureDrawingProperties nonVisualPictureDrawingProperties1 = new Pic.NonVisualPictureDrawingProperties();

            nonVisualPictureDrawingProperties1.Append(new A.PictureLocks() { NoChangeAspect = true, NoChangeArrowheads = true });

            nonVisualPictureProperties1.Append(new Pic.NonVisualDrawingProperties() { Id = (UInt32Value)0U, Name = "Picture 1", Description = "http://bueamrweb03/smp/images/blue.sm.jpg" });
            nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);

            Pic.BlipFill blipFill1 = new Pic.BlipFill();

            A.Stretch stretch1 = new A.Stretch();
            stretch1.Append(new A.FillRectangle());

            blipFill1.Append(new A.Blip() { Embed = "rId4" });
            blipFill1.Append(new A.SourceRectangle());
            blipFill1.Append(stretch1);

            Pic.ShapeProperties shapeProperties1 = new Pic.ShapeProperties() { BlackWhiteMode = A.BlackWhiteModeValues.Auto };

            A.Transform2D transform2D1 = new A.Transform2D();

            transform2D1.Append(new A.Offset() { X = 0L, Y = 0L });
            transform2D1.Append(new A.Extents() { Cx = 809625L, Cy = 485775L });

            A.PresetGeometry presetGeometry1 = new A.PresetGeometry() { Preset = A.ShapeTypeValues.Rectangle };
            A.AdjustValueList adjustValueList1 = new A.AdjustValueList();

            presetGeometry1.Append(adjustValueList1);

            A.Outline outline1 = new A.Outline() { Width = 9525 };

            outline1.Append(new A.NoFill());
            outline1.Append(new A.Miter() { Limit = 800000 });
            outline1.Append(new A.HeadEnd());
            outline1.Append(new A.TailEnd());

            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(presetGeometry1);
            shapeProperties1.Append(new A.NoFill());
            shapeProperties1.Append(outline1);

            picture1.Append(nonVisualPictureProperties1);
            picture1.Append(blipFill1);
            picture1.Append(shapeProperties1);

            graphicData1.Append(picture1);

            graphic1.Append(graphicData1);

            inline1.Append(extent1);
            inline1.Append(effectExtent1);
            inline1.Append(docProperties1);
            inline1.Append(nonVisualGraphicFrameDrawingProperties1);
            inline1.Append(graphic1);

            drawing1.Append(inline1);

            return drawing1;
        }
        private Picture PictureNew()
        {
            Picture picture3 = new Picture();
            V.Rectangle rectangle2 = new V.Rectangle() { Id = "_x0000_i1026", Style = "width:0;height:.75pt", Horizontal = true, HorizontalStandard = true, HorizontalAlignment = Ovml.HorizontalRuleAlignmentValues.Center, FillColor = "gray", Stroked = false };
            picture3.Append(rectangle2);
            return picture3;
        }
        private Paragraph ParagraphNew(JustificationValues nJustificationValuesValue, string sFontSizeComplexScriptVal, bool bBold, bool bVanish, string sText, string sFontSizeVal, bool bNoProof, bool bColor)
        {
            Paragraph paragraph2 = new Paragraph() { RsidParagraphMarkRevision = s00F57926, RsidParagraphAddition = s00F57926, RsidParagraphProperties = s00F57926, RsidRunAdditionDefault = s00F57926 };
            paragraph2.Append(ParagraphPropertiesNew(nJustificationValuesValue, sFontSizeComplexScriptVal, bBold, bVanish));
            paragraph2.Append(RunNew(sText, sFontSizeVal, bBold, bNoProof, bColor));
            return paragraph2;
        }
        private Paragraph ParagraphNew(JustificationValues nJustificationValuesValue, string sFontSizeComplexScriptVal, bool bBold, bool bVanish, string sFontSizeVal, bool bNoProof, bool bColor, Drawing drawing1)
        {
            Paragraph paragraph2 = new Paragraph() { RsidParagraphMarkRevision = s00F57926, RsidParagraphAddition = s00F57926, RsidParagraphProperties = s00F57926, RsidRunAdditionDefault = s00F57926 };
            paragraph2.Append(ParagraphPropertiesNew(nJustificationValuesValue, sFontSizeComplexScriptVal, bBold, bVanish));
            paragraph2.Append(RunNew(sFontSizeVal, bBold, bNoProof, bColor, drawing1));
            return paragraph2;
        }
        private Paragraph ParagraphNew(JustificationValues nJustificationValuesValue, string sFontSizeComplexScriptVal, bool bBold, bool bVanish, string sFontSizeVal, bool bNoProof, bool bColor, Picture picture2)
        {
            Paragraph paragraph2 = new Paragraph() { RsidParagraphMarkRevision = s00F57926, RsidParagraphAddition = s00F57926, RsidParagraphProperties = s00F57926, RsidRunAdditionDefault = s00F57926 };
            paragraph2.Append(ParagraphPropertiesNew(nJustificationValuesValue, sFontSizeComplexScriptVal, bBold, bVanish));
            paragraph2.Append(RunNew(sFontSizeVal, bBold, bNoProof, bColor, picture2));
            return paragraph2;
        }
        private TableCellProperties TableCellPropertiesNew(string sTableCellWidth, TableWidthUnitValues nTableWidthUnitValues, TableVerticalAlignmentValues nTableVerticalAlignmentValues, int nGridSpanVal)
        {
            TableCellProperties tableCellProperties2 = new TableCellProperties();
            tableCellProperties2.Append(new TableCellWidth() { Width = sTableCellWidth, Type = TableWidthUnitValues.Pct });
            if (nGridSpanVal > 0) tableCellProperties2.Append(new GridSpan() { Val = nGridSpanVal });
            tableCellProperties2.Append(new TableCellVerticalAlignment() { Val = nTableVerticalAlignmentValues });
            tableCellProperties2.Append(new HideMark());
            return tableCellProperties2;
        }
        private TableRowProperties TableRowPropertiesNew()
        {
            TableRowProperties tableRowProperties1 = new TableRowProperties();
            tableRowProperties1.Append(new TableCellSpacing() { Width = "0", Type = TableWidthUnitValues.Dxa });
            tableRowProperties1.Append(new TableJustification() { Val = TableRowAlignmentValues.Center });
            return tableRowProperties1;
        }
        private TableCell TableCellNew(JustificationValues nJustificationValuesValue, string sFontSizeComplexScriptVal, bool bBold, bool bVanish, string sText, string sFontSizeVal, bool bNoProof, bool bColor, string sTableCellWidth, TableWidthUnitValues nTableWidthUnitValues, TableVerticalAlignmentValues nTableVerticalAlignmentValues, int nGridSpanVal)
        {
            TableCell tableCell1 = new TableCell();
            tableCell1.Append(TableCellPropertiesNew(sTableCellWidth, nTableWidthUnitValues, nTableVerticalAlignmentValues, nGridSpanVal));
            tableCell1.Append(ParagraphNew(nJustificationValuesValue, sFontSizeComplexScriptVal, bBold, bVanish, sText, sFontSizeVal, bNoProof, bColor));
            return tableCell1;
        }
        private TableCell TableCellNew(JustificationValues nJustificationValuesValue, string sFontSizeComplexScriptVal, bool bBold, bool bVanish, string sFontSizeVal, bool bNoProof, bool bColor, Drawing drawing1, string sTableCellWidth, TableWidthUnitValues nTableWidthUnitValues, TableVerticalAlignmentValues nTableVerticalAlignmentValues, int nGridSpanVal)
        {
            TableCell tableCell1 = new TableCell();
            tableCell1.Append(TableCellPropertiesNew(sTableCellWidth, nTableWidthUnitValues, nTableVerticalAlignmentValues, nGridSpanVal));
            tableCell1.Append(ParagraphNew(nJustificationValuesValue, sFontSizeComplexScriptVal, bBold, bVanish, sFontSizeVal, bNoProof, bColor, drawing1));
            return tableCell1;
        }
        private TableCell TableCellNew(JustificationValues nJustificationValuesValue, string sFontSizeComplexScriptVal, bool bBold, bool bVanish, string sFontSizeVal, bool bNoProof, bool bColor, Picture picture2, string sTableCellWidth, TableWidthUnitValues nTableWidthUnitValues, TableVerticalAlignmentValues nTableVerticalAlignmentValues, int nGridSpanVal)
        {
            TableCell tableCell1 = new TableCell();
            tableCell1.Append(TableCellPropertiesNew(sTableCellWidth, nTableWidthUnitValues, nTableVerticalAlignmentValues, nGridSpanVal));
            tableCell1.Append(ParagraphNew(nJustificationValuesValue, sFontSizeComplexScriptVal, bBold, bVanish, sFontSizeVal, bNoProof, bColor, picture2));
            return tableCell1;
        }
        private TableRow TableRowNew_AddDetailItem(string sProductID, string sProduct, string sObservation, string sAmount)
        {
            TableRow tableRow12 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = s00F57926 };
            tableRow12.Append(TableRowPropertiesNew());
            tableRow12.Append(TableCellNew(JustificationValues.Left, "17", false, false, sProductID, "17", false, true, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 0));
            tableRow12.Append(TableCellNew(JustificationValues.Center, "17", false, false, sProduct, "17", false, true, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 0));
            tableRow12.Append(TableCellNew(JustificationValues.Left, "17", false, false, sObservation, "17", false, true, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 0));
            tableRow12.Append(TableCellNew(JustificationValues.Right, "17", false, false, sAmount, "17", false, true, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 0));
            return tableRow12;
        }
        private Table TableNew_Head(string sTypeSolicitor, string sSolicitude, string sDate, string sSolicitorFirstName, string sSolicitorLastName, string sDepartment, string sSolicitudeType, string dtExpiration)
        {
            Table table1 = new Table();

            TableCellMarginDefault tableCellMarginDefault1 = new TableCellMarginDefault();
            tableCellMarginDefault1.Append(new TopMargin() { Width = "15", Type = TableWidthUnitValues.Dxa });
            tableCellMarginDefault1.Append(new TableCellLeftMargin() { Width = 15, Type = TableWidthValues.Dxa });
            tableCellMarginDefault1.Append(new BottomMargin() { Width = "15", Type = TableWidthUnitValues.Dxa });
            tableCellMarginDefault1.Append(new TableCellRightMargin() { Width = 15, Type = TableWidthValues.Dxa });

            TableProperties tableProperties1 = new TableProperties();
            tableProperties1.Append(new TableWidth() { Width = "9375", Type = TableWidthUnitValues.Dxa });
            tableProperties1.Append(new TableJustification() { Val = TableRowAlignmentValues.Center });
            tableProperties1.Append(new TableCellSpacing() { Width = "0", Type = TableWidthUnitValues.Dxa });
            tableProperties1.Append(tableCellMarginDefault1);
            tableProperties1.Append(new TableLook() { Val = "04A0" });

            TableGrid tableGrid1 = new TableGrid();
            tableGrid1.Append(new GridColumn() { Width = "2085" });
            tableGrid1.Append(new GridColumn() { Width = "2501" });
            tableGrid1.Append(new GridColumn() { Width = "3439" });
            tableGrid1.Append(new GridColumn() { Width = "1350" });

            TableRow tableRow1 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = s00F57926 };
            tableRow1.Append(TableRowPropertiesNew());
            tableRow1.Append(TableCellNew(JustificationValues.Center, "21", true, false, sTypeSolicitor, "21", false, true, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 4));

            TableRow tableRow2 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = "005C238E" };
            tableRow2.Append(TableRowPropertiesNew());
            tableRow2.Append(TableCellNew(JustificationValues.Left, "21", true, false, "Nro. Solicitud:", "21", false, true, "1112", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow2.Append(TableCellNew(JustificationValues.Left, "21", true, false, sSolicitude, "21", false, true, "1334", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow2.Append(TableCellNew(JustificationValues.Right, "24", false, false, sDate, "24", false, false, "1834", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow2.Append(TableCellNew(JustificationValues.Right, "24", false, false, "24", false, false, DrawingNew(), "720", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));

            TableRow tableRow3 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = "s00F57926" };
            tableRow3.Append(TableRowPropertiesNew());
            tableRow3.Append(TableCellNew(JustificationValues.Left, "21", true, false, "Nombre:", "21", false, true, "0", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow3.Append(TableCellNew(JustificationValues.Left, "21", true, false, sSolicitorFirstName, "21", false, true, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 3));

            TableRow tableRow4 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = "s00F57926" };
            tableRow4.Append(TableRowPropertiesNew());
            tableRow4.Append(TableCellNew(JustificationValues.Left, "21", true, false, "Apellido:", "21", false, true, "0", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow4.Append(TableCellNew(JustificationValues.Left, "21", true, false, sSolicitorLastName, "21", false, true, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 3));

            //Neha
            //Added for application type 21-10-2013
            TableRow tableRow5 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = "005C238E" };
            tableRow5.Append(TableRowPropertiesNew());
            tableRow5.Append(TableCellNew(JustificationValues.Left, "21", true, false, "Tratamiento:", "21", false, true, "1112", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow5.Append(TableCellNew(JustificationValues.Left, "21", true, false, sSolicitudeType, "21", false, true, "1334", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));

            //Added for expiration date type 21-10-2013
            TableRow tableRow6 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = "005C238E" };
            tableRow6.Append(TableRowPropertiesNew());
            tableRow6.Append(TableCellNew(JustificationValues.Left, "21", true, false, "Fecha Expiracion:", "21", false, true, "1112", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow6.Append(TableCellNew(JustificationValues.Left, "21", true, false, dtExpiration, "21", false, true, "1334", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            
            TableRow tableRow7 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = s00F57926 };
            tableRow7.Append(TableRowPropertiesNew());
            tableRow7.Append(TableCellNew(JustificationValues.Left, "24", false, false, "24", false, false, PictureNew(), "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 4));

            TableRow tableRow8 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = s00F57926 };
            tableRow8.Append(TableRowPropertiesNew());
            tableRow8.Append(TableCellNew(JustificationValues.Left, "24", false, false, "Teléfono:", "20", false, false, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 0));
            tableRow8.Append(TableCellNew(JustificationValues.Left, "24", false, false, "", "24", false, false, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 3));

            TableRow tableRow9 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = s00F57926 };
            tableRow9.Append(TableRowPropertiesNew());
            tableRow9.Append(TableCellNew(JustificationValues.Left, "24", false, false, "Departamento: ", "20", false, false, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 0));
            tableRow9.Append(TableCellNew(JustificationValues.Left, "24", false, false, sDepartment, "20", false, false, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 3));

            TableRow tableRow10 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = s00F57926 };
            tableRow10.Append(TableRowPropertiesNew());
            tableRow10.Append(TableCellNew(JustificationValues.Left, "24", false, false, "Observaciones:", "20", false, false, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 0));
            tableRow10.Append(TableCellNew(JustificationValues.Left, "24", false, false, "", "24", false, false, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 3));

            TableRow tableRow11 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = s00F57926 };
            tableRow11.Append(TableRowPropertiesNew());
            tableRow11.Append(TableCellNew(JustificationValues.Left, "24", false, false, "24", false, false, PictureNew(), "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 4));

            table1.Append(tableProperties1);
            table1.Append(tableGrid1);
            table1.Append(tableRow1);
            table1.Append(tableRow2);
            table1.Append(tableRow3);
            table1.Append(tableRow4);
            table1.Append(tableRow5);
            table1.Append(tableRow6);
            table1.Append(tableRow7);
            table1.Append(tableRow8);
            table1.Append(tableRow9);
            //Neha
            //Added for type 21-10-2013
            table1.Append(tableRow10);
            table1.Append(tableRow11);

            return table1;
        }
        private Table TableNew_Detail()
        {
            Table table2 = new Table();

            TableCellMarginDefault tableCellMarginDefault2 = new TableCellMarginDefault();
            tableCellMarginDefault2.Append(new TopMargin() { Width = "15", Type = TableWidthUnitValues.Dxa });
            tableCellMarginDefault2.Append(new TableCellLeftMargin() { Width = 15, Type = TableWidthValues.Dxa });
            tableCellMarginDefault2.Append(new BottomMargin() { Width = "15", Type = TableWidthUnitValues.Dxa });
            tableCellMarginDefault2.Append(new TableCellRightMargin() { Width = 15, Type = TableWidthValues.Dxa });

            TableProperties tableProperties2 = new TableProperties();
            tableProperties2.Append(new TableWidth() { Width = "9375", Type = TableWidthUnitValues.Dxa });
            tableProperties2.Append(new TableJustification() { Val = TableRowAlignmentValues.Center });
            tableProperties2.Append(new TableCellSpacing() { Width = "0", Type = TableWidthUnitValues.Dxa });
            tableProperties2.Append(tableCellMarginDefault2);
            tableProperties2.Append(new TableLook() { Val = "04A0" });

            TableGrid tableGrid2 = new TableGrid();
            tableGrid2.Append(new GridColumn() { Width = "937" });
            tableGrid2.Append(new GridColumn() { Width = "4688" });
            tableGrid2.Append(new GridColumn() { Width = "2344" });
            tableGrid2.Append(new GridColumn() { Width = "1406" });

            TableRow tableRow10 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = s00F57926 };
            tableRow10.Append(TableRowPropertiesNew());
            tableRow10.Append(TableCellNew(JustificationValues.Center, "21", true, false, "LISTA DE PRODUCTOS", "21", false, true, "0", TableWidthUnitValues.Auto, TableVerticalAlignmentValues.Center, 4));

            TableRow tableRow11 = new TableRow() { RsidTableRowMarkRevision = s00F57926, RsidTableRowAddition = s00F57926, RsidTableRowProperties = s00F57926 };
            tableRow11.Append(TableRowPropertiesNew());
            tableRow11.Append(TableCellNew(JustificationValues.Left, "24", true, false, "Código", "24", false, false, "500", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow11.Append(TableCellNew(JustificationValues.Center, "24", true, false, "Descripción", "24", false, false, "2500", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow11.Append(TableCellNew(JustificationValues.Left, "24", true, false, "Requisitos", "24", false, false, "1250", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));
            tableRow11.Append(TableCellNew(JustificationValues.Right, "24", true, false, "Cant Solic", "24", false, false, "750", TableWidthUnitValues.Pct, TableVerticalAlignmentValues.Center, 0));

            table2.Append(tableProperties2);
            table2.Append(tableGrid2);
            table2.Append(tableRow10);
            table2.Append(tableRow11);
            return table2;
        }
        private Paragraph ParagraphNew_BreakValuesPage()
        {
            Paragraph paragraph29 = new Paragraph() { RsidParagraphAddition = "000A1AAF", RsidRunAdditionDefault = "000A1AAF" };
            Run run27 = new Run();
            run27.Append(new Break() { Type = BreakValues.Page });
            paragraph29.Append(run27);
            return paragraph29;

        }
        private Paragraph ParagraphNew_EndHead()
        {
            Paragraph paragraph18 = new Paragraph() { RsidParagraphMarkRevision = s00F57926, RsidParagraphAddition = s00F57926, RsidParagraphProperties = s00F57926, RsidRunAdditionDefault = s00F57926 };
            paragraph18.Append(ParagraphPropertiesNew(JustificationValues.Left, "24", false, true));
            return paragraph18;
        }
        private SectionProperties SectionPropertiesNew()
        {
            SectionProperties sectionProperties1 = new SectionProperties() { RsidR = "008B3A2F", RsidSect = "008B3A2F" };
            sectionProperties1.Append(new PageSize() { Width = (UInt32Value)12240U, Height = (UInt32Value)15840U });
            sectionProperties1.Append(new PageMargin() { Top = 1417, Right = (UInt32Value)1701U, Bottom = 1417, Left = (UInt32Value)1701U, Header = (UInt32Value)708U, Footer = (UInt32Value)708U, Gutter = (UInt32Value)0U });
            sectionProperties1.Append(new Columns() { Space = "708" });
            sectionProperties1.Append(new DocGrid() { LinePitch = 360 });
            return sectionProperties1;
        }

    }
}

