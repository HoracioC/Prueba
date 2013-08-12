<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/jquery-1.4.2.mins.js" type="text/javascript"></script>
    <script src="js/jquery.timers.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $().ready(function () {
            $(document).everyTime(30000, function () {
                $.ajax({
                    type: "POST", url: "Default.aspx/KeepActiveSession", data: {}, contentType: "application/json; charset=utf-8",
                    dataType: "json", async: true, success: VerifySessionState, error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus + ": " + XMLHttpRequest.responseText);
                    }
                });
            });
        });
        var cantValidaciones = 0;
        function VerifySessionState(result) {
            if (result.d) {
                $("#EstadoSession").text("activo");
            }
            else
                $("#EstadoSession").text("expiro");

            $("#cantValidaciones").text(cantValidaciones);
            cantValidaciones++;
        }
    </script>
    <script type="text/javascript">

        var validar_longitud = function (texto, num_caracteres_permitidos) {
            num_caracteres = texto.value.length
            if (num_caracteres > num_caracteres_permitidos) {
                texto.value = texto.value.substring(0, (num_caracteres_permitidos))
            }

        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:UpdatePanel runat="server" ID="UpUP">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
                <ProgressTemplate>
                    <div class="DivConteUP">
                    </div>
                    <div class="DivUpPro">
                        <table class="TablaUpPro">
                            <tr>
                                <td style="text-align: inherit;">
                                    <img src="imagenes/ProgressBar.gif" alt="Loading" />
                                </td>
                                <td style="text-align: inherit;">
                                    <span style="font-family: Sans-Serif; font-size: medium; font-weight: bold; font">Cargando...</span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="Contenido">
                <div class="Letrero">
                    Reportes > Calificiaciones</div>
                    <div class="DivGrid">
                                <div style="border: 2px solid; border-color: #009B0D; width: 900px;">
                                    <div id="Div1" runat="server" class="container">
                <asp:GridView ID="TaskGridView" runat="server" 
        AutoGenerateEditButton="True" 
        AllowPaging="true"
        OnRowEditing="TaskGridView_RowEditing"         
        OnRowCancelingEdit="TaskGridView_RowCancelingEdit" 
        OnRowUpdating="TaskGridView_RowUpdating"
        OnPageIndexChanging="TaskGridView_PageIndexChanging">
      </asp:GridView>
                                </div></div></div>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>

