<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra2.Master" AutoEventWireup="true" CodeBehind="Modulos.aspx.cs" Inherits="S_Presentacion.Usuarios.Modulos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Modulos</title>
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
    <script type="text/javascript">
                function soloNumero(evt) {           
                    evt = (evt) ? evt : window.event;
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
        }
                function soloNumeroPrecio(evt,obj) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var commacontains = value.indexOf(",") != -1;
            if (charCode == 44) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
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
        <li class="breadcrumb-item active">Permisos por Tipo de Usuarios</li>
    </ol>
    <div class="card-body">
        <div class="container">
            <div class="card card-register mx-auto mt-5">
                <div class="card-header">Cotizacion</div>
                <div class="card-body">
                    <div class="form-group">
                        <div class="form-row">
                            <div class="col-md-6">
                                <label for="txtID">ID</label>
                                <input class="form-control" id="txtID" type="text" aria-describedby="nameHelp" runat="server" onkeypress="return soloNumero(event)" placeholder="Ingrese ID del tipo de usuario" required>
                            </div>
                            <div class="col-md-6">
                                <label for="txtRut">Modulos</label>
                                <input class="form-control" id="txtModulos" type="text" aria-describedby="nameHelp" runat="server" onkeypress="return soloNumeroPrecio(event,this)" placeholder="Ingrese los Modulos (ej: 1 o 1,2,3,..)" required>
                            </div>
                        </div>
                    </div>
                    <!--
                <a class="btn btn-primary btn-block" href="login.html">Register</a>
                <div class="text-center">
                    <a class="d-block small mt-3" href="login.html">Login Page</a>
                    <a class="d-block small" href="forgot-password.html">Forgot Password?</a>
                </div>
                -->
                    <asp:Button CssClass="btn btn-dark" ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" />
                </div>
            </div>
        </div>
    </div>
    <%-- GridView para Modificar y Bloquear Usuarios --%>
    <div class="card-body">
        <div class="table-responsive">
            <asp:GridView ID="gvListaModulos" CssClass="table table-bordered" DataKeyNames="ID_tipo_usuario" runat="server"  AutoGenerateColumns="False" DataSourceID="odsModulos">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="ID_tipo_usuario" HeaderText="ID Tipo Usuario" SortExpression="ID_tipo_usuario" />
                    <asp:BoundField DataField="Modulos" HeaderText="Modulos" SortExpression="Modulos" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsModulos" runat="server" DataObjectTypeName="S_Negocio.PermisosModulos" DeleteMethod="Delete" InsertMethod="Create" SelectMethod="ListaProductos" TypeName="S_Negocio.ColeccionPermisosModulos" UpdateMethod="Update"></asp:ObjectDataSource>
</asp:Content>
