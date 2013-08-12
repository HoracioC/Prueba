<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout ="360000">
    </asp:ToolkitScriptManager>

<asp:Panel ID="pnlEliminarAdv" runat="server" defaultbutton="btnDeleteOk" style="display:none">
<asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
	                                        targetcontrolid="btnError" popupcontrolid="pnlEliminarAdv" backgroundcssclass="ModalPopupBG">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnError" runat="server" Text="Button" CssClass="invisible" />

            <asp:UpdatePanel ID="upnlEliminado" runat="server">
                <ContentTemplate>
                    <div class="modal-table-delete">
                        <div class="modal-table-caption-delete"><asp:Label ID="Label5" runat="server" Text="Advertencia"></asp:Label></div>
                            <div class="modal-table-row-delete"></div>
                                <div class="modal-table-col-delete">
                                    <asp:Label ID="lblEliminado" runat="server" Text="ADVERTENCIA LA BASE DE DATOS ES DE PRUEBAS, COMUNICARSE CON SISTEMAS PARA QUE CORRIJA ESTO !!!!"></asp:Label></div>
                                <div class="modal-table-row-delete"></div>
                                <div class="modal-table-col-delete">
                                    <asp:Button ID="btnDeleteOk" runat="server" Text="OK" CssClass="button"/></div></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

</asp:Content>
