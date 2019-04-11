<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="S_Presentacion.Inicio" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inicio</title>
    
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
        window.location = "Logout.aspx";
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
    <%--<h3>Session Idle:&nbsp;<span id="secondsIdle"></span>&nbsp;seconds.</h3>--%>
<asp:LinkButton ID="lnkFake" runat="server" />
<asp:ModalPopupExtender ID="mpeTimeout" BehaviorID ="mpeTimeout" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkFake"
    OkControlID="btnYes" CancelControlID="btnNo" BackgroundCssClass="modal" OnOkScript = "ResetSession()">
</asp:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modal-dialog" Style="display: none">
    <div class="modal-content">
        <div class="modal-header">
            <h6 class="modal-title" id="ModalClientesLabel">La sesion esta expirando...</h6>
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
      <div class="container-fluid">
          <!--<a>Bienvenido/a</a>-->
          <img src="imgs/principal.png" alt="Imagen Principal" style="max-width:100%; height: auto; float:left;"/>
      </div>
</asp:Content>
