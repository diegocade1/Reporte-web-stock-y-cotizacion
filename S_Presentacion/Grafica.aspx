<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Grafica.aspx.cs" Inherits="S_Presentacion.Grafica" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Grafico General</title>
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
    <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="Inicio.aspx">Inicio</a>
        </li>
        <li class="breadcrumb-item active">Grafica</li>
    </ol>
    <!-- Example Bar Chart Card-->
    <div class="card-header">
        <i class="fa fa-bar-chart"></i>Valor Total del Inventario
    </div>
    <asp:Chart ID="Chart1" runat="server" DataSourceID="ObjectDataSource1" Height="419px" Width="720px" Compression="50" OnClick="Chart1_Click" OnLoad="Chart1_Load">
        <Series>
            <asp:Series Name="Series1" XValueMember="Codigo" YValueMembers="Valortotal"></asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1"> 
                <AxisX Interval="1" ></AxisX>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListaTotal" TypeName="S_Negocio.ColeccionReporte"></asp:ObjectDataSource>
</asp:Content>
