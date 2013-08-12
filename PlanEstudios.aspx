<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="PlanEstudios.aspx.vb" Inherits="PlanEstudios" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout ="360000"></asp:ToolkitScriptManager>
                <asp:UpdatePanel runat="server" ID="UpUP">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
                            <ProgressTemplate>
                                  <div class="DivConteUP">
                                  </div>
                                   <div class="DivUpPro">
                                        <table class="TablaUpPro">
                                        <tr>
                                            <td style=" text-align: inherit;"><img src="imagenes/ProgressBar.gif" alt="Loading"  /></td>
                                            <td style=" text-align: inherit;"><span style="font-family: Sans-Serif; font-size: medium; font-weight: bold; font">Cargando...</span></td>
                                        </tr>
                                        </table>
                                   </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlPlanEstudios" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                        AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="1202px" ToolbarImagesFolderUrl="" 
                        ToolPanelWidth="200px" Width="1104px" PageZoomFactor="75" />
                    </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>

