<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CalificacionesAlumno.aspx.vb" Inherits="CalificacionesAlumno" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="360000">
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
                <asp:Button ID="btnmodificarcal" runat="server" Text="Modificar Calificaciones" 
                    Width="169px" />
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="950"
                    Height="785px" AutoPostBack="True" BorderStyle="None" CssClass="CustomTabStyle">
                    <asp:TabPanel ID="TabPanel1" runat="server">
                        <ContentTemplate>
                        <div class="DivGridCaption">
                                            <asp:TextBox ID="txtFiltro" runat="server" CssClass="txtboxM"></asp:TextBox>
                                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="button" /><br />
                                            <asp:Button ID="btnRecargar" runat="server" Text="Recargar" CssClass="button" />
                                            
                                            <br />
                                            <br />
                                            
                                            <asp:Label ID="lblEliminado" runat="server"></asp:Label>
                                            
                                            <br />
                                            <br />
                                        </div>
                            <div class="DivGrid">
                                <div style="border: 2px solid; border-color: #009B0D; width: 900px;">
                                    <div id="Div1" runat="server" class="container">
                                        <asp:GridView ID="gridCalificaciones" runat="server" 
                                            EnableModelValidation="True" CssClass="GridChico"
                                            AllowPaging="True" PageSize="15" AutoGenerateEditButton="True">
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div style="text-align: right">
                                <asp:Label ID="lblPaginacion" runat="server" Text="Paginación:" CssClass="labelM"></asp:Label><asp:TextBox
                                    ID="txtPaginacion" runat="server" CssClass="txtPag"></asp:TextBox><asp:Button ID="btnPaginacion"
                                    runat="server" Text="Cambiar" CssClass="button" /></div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
