<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra2.Master" AutoEventWireup="true" CodeBehind="MantenedorUsuarios.aspx.cs" Inherits="S_Presentacion.Usuarios.MantenedorUsuarios" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mantenedor de Usuarios</title>
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
        <li class="breadcrumb-item active">Registrar Usuarios</li>
    </ol>
    <%-- Campos de usuario --%>
        <div class="container">
        <div class="card card-register mx-auto mt-5">
            <div class="card-header">Registro de usuario</div>
            <div class="card-body">
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <label for="txtUsuario">Usuario</label>
                            <input class="form-control" id="txtUsuario" type="text" aria-describedby="nameHelp" runat="server" placeholder="Ingrese Usuario" required>
                        </div>
                        <div class="col-md-6">
                            <label for="txtPassword">Password&nbsp;</label>
                            <input class="form-control" id="txtPassword" type="password" aria-describedby="nameHelp" runat="server" placeholder="Ingrese Password" required>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <label for="txtContacto">Nombre</label>
                            <input class="form-control" id="txtNombre" type="text" aria-describedby="nameHelp" runat="server" placeholder="Ingrese Nombre de Usuario" required>
                        </div>
                        <div class="col-md-6">
                            <label for="txtFecha_">Apellido</label>
                            <input class="form-control" id="txtApellido" type="text" aria-describedby="nameHelp" runat="server" placeholder="Ingrese Apellido de Usuario" required>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <label for="txtCorreo">Tipo de Usuario</label>
                            <asp:DropDownList CssClass="form-control" ID="ddlTipoUsuario" runat="server" DataSourceID="odsTipoUsuario" DataTextField="Descripcion" DataValueField="Descripcion" AppendDataBoundItems="true">
                                <asp:ListItem Selected="false" Text="--Seleccione Tipo Usuario" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <label for="txtCargo">Cargo</label>
                            <input class="form-control" id="txtCargo" type="text" runat="server" placeholder="Ingrese Cargo" required>
                        </div>
                        <div class="col-md-6">
                            <label for="txtEstado">Estado</label>
                            <asp:DropDownList CssClass="form-control" ID="ddlEstado" runat="server">
                                <asp:ListItem Selected="false" Text="--Seleccione Estado" Value=""></asp:ListItem>
                                <asp:ListItem Selected="false" Text="Habilitado" Value="Habilitado"></asp:ListItem>
                                <asp:ListItem Selected="false" Text="Deshabilitado" Value="Deshabilitado"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnRegistrar" CssClass="btn btn-primary" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" />
                <a class="d-block mt-3" href="Lista.aspx">Lista de Usuarios</a>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsTipoUsuario" runat="server" SelectMethod="ListaTipoUsuarios" TypeName="S_Negocio.ColeccionTipoUsuario"></asp:ObjectDataSource>
</asp:Content>
