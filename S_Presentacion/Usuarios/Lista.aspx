<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra2.Master" AutoEventWireup="true" CodeBehind="Lista.aspx.cs" Inherits="S_Presentacion.Usuarios.Lista" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Lista</title>
                        <script type="text/javascript">
function SessionExpireAlert(timeout) {
    var seconds = timeout / 1000;
    document.getElementsByName("secondsIdle").innerHTML = seconds;
    document.getElementsByName("seconds").innerHTML = seconds;
    setInterval(function () {
        seconds--;
        document.getElementById("seconds").innerHTML = seconds;
        document.getElementById("secondsIdle").innerHTML = seconds;
    }, 1000);
    setTimeout(function () {
        //Show Popup before 20 seconds of timeout.
        $find("mpeTimeout").show();
    }, timeout - 60000);
    setTimeout(function () {
        window.location = "../Logout.aspx";
    }, timeout);
};
function ResetSession() {
    //Redirect to refresh Session.
    window.location = window.location.href;
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <!-- html de sesion -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h3 style="font-size: 0px; margin: 0px;">Session Idle:&nbsp;<span id="secondsIdle"></span>&nbsp;seconds.</h3>
    <asp:LinkButton ID="lnkFake" runat="server" />
    <asp:ModalPopupExtender ID="mpeTimeout" BehaviorID="mpeTimeout" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
        OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modal" OnOkScript="ResetSession()">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title" id="Modaltitulo">La sesion esta expirando...</h6>
            </div>
            <div class="modal-body">
                La sesión va a expirar dentro de&nbsp;<span id="seconds"></span>&nbsp;segundos.<br />
                ¿Desea recargar la sesión?
            </div>
            <div class="modal-footer" style="text-align: right">
                <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="yes" />
                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="no" />
            </div>
        </div>
    </asp:Panel>
    <!-- /html de sesion -->
	<ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="../Inicio.aspx">Inicio</a>
        </li>
        <li class="breadcrumb-item">
            <a>Usuario</a>
        </li>
        <li class="breadcrumb-item">
            <a href="MantenedorUsuarios.aspx">Registrar Usuarios</a>
        </li>
        <li class="breadcrumb-item active">Lista de Usuarios</li>
    </ol>
    <%-- GridView para Modificar y Bloquear Usuarios --%>
    <div class="card-body">
        <div class="table-responsive">
            <asp:GridView ID="gvListaUsuarios" CssClass="table table-bordered" DataKeyNames="ID" runat="server" AutoGenerateColumns="False" DataSourceID="odsListaUsuarios">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true"/>
                    <asp:BoundField DataField="Usuario_" HeaderText="Usuario" SortExpression="Usuario_"  ReadOnly="true"/>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" />
                    <asp:TemplateField HeaderText="Password" SortExpression="Password">
						<EditItemTemplate>
							<asp:TextBox ID="txtPassword" runat="server" Text='<%# Bind("Password") %>'></asp:TextBox>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:Label ID="lblPassword" runat="server" Text='<%# Bind("Password") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo_usuario" SortExpression="Tipo_usuario">
						<EditItemTemplate>
							<%--<asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Tipo_usuario") %>'></asp:TextBox>--%>
							<asp:DropDownList ID="ddlTipoUsuario" runat="server" DataSourceID="odsTipoUsuario" DataTextField="Descripcion" DataValueField="Descripcion" SelectedValue='<%# Bind("Tipo_usuario") %>' AppendDataBoundItems="True">
								<asp:ListItem Selected="false" Text="--Seleccione Tipo Usuario" Value=""></asp:ListItem>
							</asp:DropDownList>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:Label ID="lblTipoUsuario" runat="server" Text='<%# Bind("Tipo_usuario") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
                    <asp:BoundField DataField="Cargo" HeaderText="Cargo" SortExpression="Cargo" />
                    <asp:TemplateField HeaderText="Estado" SortExpression="Estado">
						<EditItemTemplate>
							<%--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Estado") %>'></asp:TextBox>--%>
							<asp:DropDownList ID="ddlEstado" runat="server" SelectedValue='<%# Bind("Estado") %>'>
                                <asp:ListItem Text="Habilitado" Value="Habilitado"></asp:ListItem>
                                <asp:ListItem Text="Deshabilitado" Value="Deshabilitado"></asp:ListItem>
                            </asp:DropDownList>
						</EditItemTemplate>
						<ItemTemplate>
							<asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsListaUsuarios" runat="server" DataObjectTypeName="S_Negocio.Usuario" DeleteMethod="Delete" SelectMethod="ListaUsuarios" TypeName="S_Negocio.ColeccionUsuarios" UpdateMethod="Update"></asp:ObjectDataSource>
	<asp:ObjectDataSource ID="odsTipoUsuario" runat="server" SelectMethod="ListaTipoUsuarios" TypeName="S_Negocio.ColeccionTipoUsuario"></asp:ObjectDataSource>
</asp:Content>
