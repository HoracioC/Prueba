<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="NombreCarrera.aspx.vb" Inherits="NombreCarrera" %>

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
                        
                        <div class="Contenido">
                            <div class = "Letrero">
                                Adminstración de Catalgos > Nombre de Carrera
                            </div>
                            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="3" 
                                Width="950" Height="785px" AutoPostBack="True" 
                                BorderStyle="None" CssClass="CustomTabStyle">
                                <asp:TabPanel ID="TabPanel1" runat="server">
                                    <ContentTemplate>
                                    <div class="DivGrid">
                                        <div class="DivGridCaption">
                                            <asp:TextBox ID="txtFiltro" runat="server" ValidationGroup="NuloPlan" CssClass="txtboxM"></asp:TextBox>
                                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="button" /><br />
                                            <asp:Button ID="btnRecargar" runat="server" Text="Recargar" CssClass="button" />
                                            <asp:Button ID="btnAddNombreCarrera" runat="server" Text="Agregar Nombre de Carrera" CssClass="button" />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                        </div>

                                        <div style="border: 2px solid; border-color: #009B0D;  width:900px;">
                                            <div id="Div1" runat="server" class = "container">
                                                <asp:GridView ID="gridNombreCarreras" runat="server" 
                                                    CssClass="Grid" AllowPaging="True" AutoGenerateColumns="False" PageSize="13" 
                                                    EnableModelValidation="True" onsorting="gridviewSorting_Sorting"
                                                    AllowSorting="True" >
                                                    <PagerStyle Font-Size="12pt" BackColor="#57AC63" ForeColor="White" 
                                                        CssClass="Separacion"/> 
                                                    <HeaderStyle  CssClass="grdview_headers" ForeColor="White"  />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDeleteNombreCarrera" runat="server" Text="Eliminar" CommandName="Borrar" ToolTip="Eliminar Nombre de Carrera"><img src="imagenes/trashimg.png" alt="Eliminar Nombre de Carrera" class="BtnGrid"/></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbtnEditNombreCarrera" runat="server" Text="Editar" CommandName="Editar" ToolTip="Editar Nombre de Carrera"><img src="imagenes/editimg.png" alt="Editar Nombre de Carrera" width="30px" height="30px" class="BtnGrid"/></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="70px" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="id_Nombre_Carrera" HeaderText="id_Nombre_Carrera" 
                                                                SortExpression="id_Nombre_Carrera" ReadOnly="True">
                                                            <HeaderStyle Width="0px" />
                                                            <ItemStyle Width="0px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Nombre_Carrera" HeaderText="Nombre Carrera" 
                                                                SortExpression="Nombre_Carrera" ReadOnly="True">
                                                            <HeaderStyle Width="593px" />
                                                            <ItemStyle Width="593px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Abreviatura" HeaderText="Abreviatura" SortExpression="Abreviatura" 
                                                                ReadOnly="True">
                                                            <HeaderStyle Width="200px" />
                                                            <ItemStyle Width="200px" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                    <RowStyle Wrap="False" />
                                                </asp:GridView>
                                           </div>
                                        </div>
                                        <div style="text-align:right">
                                            <asp:Label ID="lblPaginacion" runat="server" Text="Paginación:" CssClass="labelM"></asp:Label>
                                            <asp:TextBox ID="txtPaginacion" runat="server" CssClass="txtPag"></asp:TextBox>
                                            <asp:Button ID="btnPaginacion" runat="server" Text="Cambiar" CssClass="button" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanel2" runat="server">
                                    <ContentTemplate>
                                        <table class="TablaModal">
                                            <tr>
                                                <th colspan="2">
                                                    <asp:Label ID="lblNombreCarrera" runat="server" Text="Nuevo Nombre de Carrera"></asp:Label>
                                                    <div class="imageClose">
                                                        <asp:ImageButton ID="ibtnCerrarNombreCarrera" runat="server" ImageUrl="images/close.png" CssClass="botonCerrar"/>
                                                    </div>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNombreCarreraText" runat="server" Text="Nombre de Carrera" CssClass="labelM"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNombreCarrera" runat="server" ValidationGroup="NuloAddNMat" 
                                                        CssClass="txtboxM" Width="600px" TextMode="MultiLine" 
                                                        onKeyUp="validar_longitud(this,100)"></asp:TextBox>
                                                    <br /><asp:RequiredFieldValidator ID="ReqtxtNombreCarrera" runat="server" ControlToValidate="txtNombreCarrera"
                                                    ErrorMessage="El nombre de la carrera debe tener información." ValidationGroup="NuloAddNMat"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegtxtCaracteres" runat="server" ControlToValidate="txtNombreCarrera"
                                                    ErrorMessage="Máximo 100 caracteres." ValidationGroup="NuloAddNMat" 
                                                        ValidationExpression=".{0,100}" Width="140px"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAbreviatura" runat="server" CssClass="labelM" Text="Abreviatura"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAbreviatura" runat="server" ValidationGroup="NuloAddNMat" CssClass="txtboxM" onKeyUp="validar_longitud(this,5)"></asp:TextBox>
                                                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAbreviatura"
                                                    ErrorMessage="La abreviatura debe tener información." ValidationGroup="NuloAddNMat"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtAbreviatura"
                                                    ErrorMessage="Máximo 5 caracteres." ValidationGroup="NuloAddNMat" 
                                                        ValidationExpression=".{0,5}" Width="140px"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr align="right">
                                                <td colspan="2"> 
                                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button"/>
                                                        <asp:Button ID="btnGuardarNombreCarrera" runat="server" Text="Guardar" CssClass="button" ValidationGroup="NuloAddNMat"/>
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
                                                <asp:Label ID="Label1" runat="server" Text="Eliminar Nombre de Carrera"></asp:Label>
                                                <div class="imageClose">
                                                    <asp:ImageButton ID="ibtnCerrarEliminar" runat="server" ImageUrl="images/close.png" CssClass="botonCerrar" />
                                                </div>
                                            </th>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblValoridNombreCarrera" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblNombreCarreraDel" runat="server" Text="Label" CssClass="labelM"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="btnCancelarE" runat="server" Text="Cancelar" CssClass="button"/>
                                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="button"/>
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
                                                <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="button"/>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                </asp:TabPanel>
                            </asp:TabContainer>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>

