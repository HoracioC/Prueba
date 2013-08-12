<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Materia.aspx.vb" Inherits="Materia" MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
        contenido_textarea = "";
        num_caracteres_permitidos = 200;
        var validar_longitud = function (texto) {

            num_caracteres = texto.value.length

            if (num_caracteres < num_caracteres_permitidos) {
                contenido_textarea = texto.value
            } else {
                texto.value = contenido_textarea
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
                    Proceso de Inscripcion > Materias</div>
                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="950"
                    Height="785px" AutoPostBack="True" BorderStyle="None" CssClass="CustomTabStyle">
                    <asp:TabPanel ID="TabPanel1" runat="server">
                        <ContentTemplate>
                            <div class="DivGrid">
                                <div class="DivGridCaption">
                                    <asp:TextBox ID="txtFiltro" runat="server" ValidationGroup="NuloPlan" CssClass="txtboxM"></asp:TextBox>
                                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="button" />
                                    <asp:Button ID="btnReporte" runat="server" CssClass="button" Text="Reporte" />
                                    <br />
                                    <asp:Button ID="btnRecargar" runat="server" Text="Recargar" CssClass="button" />
                                    <asp:Button ID="btnAddMateria" runat="server" Text="Agregar Materia" CssClass="button" />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </div>
                                <div style="border: 2px solid; border-color: #009B0D; width: 900px;">
                                    <div id="Div1" runat="server" class="container">
                                        <asp:GridView ID="gridPlanE" runat="server" CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False"
                                            PageSize="13" EnableModelValidation="True" OnSorting="gridviewSorting_Sorting"
                                            AllowSorting="True">
                                            <PagerStyle Font-Size="12pt" BackColor="#57AC63" ForeColor="White" CssClass="Separacion" />
                                            <HeaderStyle CssClass="grdview_headers" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDeleteMateria" runat="server" Text="Eliminar" CommandName="Borrar"
                                                            ToolTip="Eliminar Materia"><img src="imagenes/trashimg.png" alt="Eliminar Materia" class="BtnGrid"/></asp:LinkButton>
                                                        <asp:LinkButton ID="lbtnEditMateria" runat="server" Text="Editar" CommandName="Editar"
                                                            ToolTip="Editar Materia"><img src="imagenes/editimg.png" alt="Editar Materia" width="30px" height="30px" class="BtnGrid"/></asp:LinkButton></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id_Materia" HeaderText="id_Materia" SortExpression="id_Materia"
                                                    ReadOnly="True">
                                                    <HeaderStyle Width="0px" />
                                                    <ItemStyle Width="0px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Nombre_Materia" HeaderText="Nombre Materia" SortExpression="Nombre_Materia"
                                                    ReadOnly="True">
                                                    <HeaderStyle Width="593px" />
                                                    <ItemStyle Width="593px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Horas" HeaderText="Horas" SortExpression="Horas" ReadOnly="True">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Creditos" HeaderText="Creditos" SortExpression="Creditos"
                                                    ReadOnly="True">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <RowStyle Wrap="False" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div style="text-align: right">
                                    <asp:Label ID="lblPaginacion" runat="server" Text="Paginación:" CssClass="labelM"></asp:Label>
                                    <asp:TextBox ID="txtPaginacion" runat="server" CssClass="txtPag"></asp:TextBox>
                                    <asp:Button ID="btnPaginacion" runat="server" Text="Cambiar" CssClass="button" /></div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel2" runat="server">
                        <ContentTemplate>
                            <table class="TablaModal">
                                <tr>
                                    <th colspan="2">
                                        <asp:Label ID="lblMateria" runat="server" Text="Nueva Materia"></asp:Label>
                                        <div class="imageClose">
                                            <asp:ImageButton ID="ibtnCerrarMateria" runat="server" ImageUrl="images/close.png"
                                                CssClass="botonCerrar" /></div>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMateriaText" runat="server" Text="Materia" CssClass="labelM"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMateria" runat="server" ValidationGroup="NuloAddNMat" CssClass="txtboxM"
                                            Width="600px" TextMode="MultiLine" onKeyDown="validar_longitud(this)"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="ReqtxtMateria" runat="server" ControlToValidate="txtMateria"
                                            ErrorMessage="La materia debe tener información." ValidationGroup="NuloAddNMat"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegtxtCaracteres" runat="server" ControlToValidate="txtMateria"
                                            ErrorMessage="Máximo 200 caracteres." ValidationGroup="NuloAddNMat" ValidationExpression=".{0,200}"
                                            Width="140px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblHoras" runat="server" CssClass="labelM" Text="Horas"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHoras" runat="server" ValidationGroup="NuloAddNMat" CssClass="txtboxM"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="CustomValidator1" runat="server" ControlToValidate="txtHoras"
                                            ErrorMessage="Solo se permiten valores con 2 decimales" ValidationGroup="NuloAddNMat"
                                            ValidationExpression="^\d{1,2}($|\.\d{1,2}$)" Width="210px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblCreditos" runat="server" CssClass="labelM" Text="Créditos"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCreditos" runat="server" ValidationGroup="NuloAddNMat" CssClass="txtboxM"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCreditos"
                                            ErrorMessage="Solo se permiten valores con 2 decimales" ValidationGroup="NuloAddNMat"
                                            ValidationExpression="^\d{1,2}($|\.\d{1,2}$)" Width="210px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr align="right">
                                    <td colspan="2">
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" />
                                        <asp:Button ID="btnGuardarMateria" runat="server" Text="Guardar" CssClass="button"
                                            ValidationGroup="NuloAddNMat" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel3" runat="server">
                        <ContentTemplate>
                            <table class="TablaModal">
                                <tr>
                                    <th>
                                        <asp:Label ID="Label1" runat="server" Text="Eliminar Materia"></asp:Label>
                                        <div class="imageClose">
                                            <asp:ImageButton ID="ibtnCerrarEliminar" runat="server" ImageUrl="images/close.png"
                                                CssClass="botonCerrar" /></div>
                                    </th>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblValoridMateria" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblMateriaDel" runat="server" Text="Label" CssClass="labelM"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Button ID="btnCancelarE" runat="server" Text="Cancelar" CssClass="button" />
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="button" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="TabPanel4" runat="server">
                        <ContentTemplate>
                            <table class="TablaModalError">
                                <tr>
                                    <th>
                                        <asp:Label ID="Label5" runat="server" Text="Advertencia"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEliminado" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="button" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
            
