namespace MigraPdfWrapper


open MigraDoc
open MigraDoc.DocumentObjectModel 

type CssElement  = {Name:string; Value:string}

type HtmlElement =
| Table of HtmlElement seq
| Tr of HtmlElement seq
| Td of HtmlElement seq
| THead of HtmlElement seq
| TFoot of HtmlElement seq
| Span of HtmlElement seq
| Ol of HtmlElement seq
| Ul of HtmlElement seq
| Li of HtmlElement seq
| Text of string

and HtmlTypeAndContents =
    {
        Css: CssElement seq; 
        Elements : HtmlElement seq;
    }


module Converter  = 
    open MigraDoc.DocumentObjectModel.Shapes

    let private SetUpDocument (title:string)(subject:string)(author:string) =
            let document = Document ()
            document.Info.Title <- "A sample invoice";
            document.Info.Subject <- "Demonstrates how to create an invoice.";
            document.Info.Author <-"Stefan Lange";

            document
    
    let SetupStyle (document:Document) = 
        let style = document.Styles.["Normal"];
        style.Font.Name <- "Verdana";
        (*
        let headerStyle = document.Styles.[StyleNames.Header];
        headerStyle.ParagraphFormat.AddTabStop(Unit.FromMillimeter, TabAlignment.Right);
        headerStyle <- document.headerStyles[headerStyleNames.Footer];
        headerStyle.ParagraphFormat.AddTabStop("m", TabAlignment.Center);
        headerStyle <- document.headerStyles.AddheaderStyle("Table", "Normal");
        headerStyle.Font.Name <- "Verdana";
        headerStyle.Font.Name <- "Times New Roman";
        headerStyle.Font.Size <- 8pt;

        style <- document.Styles.AddStyle("Reference", "Normal");
        style.ParagraphFormat.SpaceBefore <- "m";
        style.ParagraphFormat.SpaceAfter <- "m";
        style.ParagraphFormat.TabStops.AddTabStop("m", TabAlignment.Right);
        *)
        document

    
    let AddHeaderImage (imageLocation:string)(section:Section) = 
      // Put a logo in the header
      let image = section.Headers.Primary.AddImage(imageLocation);
      image.Height <- Unit.Parse("2.5cm");
      image.LockAspectRatio <- true;
      image.RelativeVertical <- RelativeVertical.Line;
      image.RelativeHorizontal <- RelativeHorizontal.Margin;
      image.Top <- TopPosition.op_Implicit(ShapePosition.Top);
      image.Left <- LeftPosition.op_Implicit(ShapePosition.Right);
      image.WrapFormat.Style <- WrapStyle.Through;
      section 


    let AddFooter (footer:string) (section:Section) = 
           // Create footer
          let paragraph = section.Footers.Primary.AddParagraph();
          paragraph.AddText(footer) |> ignore;
          paragraph.Format.Font.Size <- Unit.op_Implicit 9;
          paragraph.Format.Alignment <- ParagraphAlignment.Center;

          paragraph
    

    let CreateTable (section:Section) = 
        // Create the item table
        let table = section.AddTable();
        table.Style <- "Table";
//        table.Borders.Color <- TableBorder;
        table.Borders.Width <- Unit.op_Implicit(0.25);
        table.Borders.Left.Width <- Unit.op_Implicit(0.5);
        table.Borders.Right.Width <- Unit.op_Implicit(0.5);
        table.Rows.LeftIndent <- Unit.op_Implicit(0);
        
        table



    let ConvertToPdf (element : HtmlElement) =
        let document = 
                SetUpDocument "" "" "" |>
                SetupStyle
        
        let section = 
                document.AddSection() |> 
                AddHeaderImage "" 
        
        document
           
       




        