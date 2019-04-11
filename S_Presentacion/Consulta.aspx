<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="S_Presentacion.Consulta" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Consulta de Stock</title>
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
        <li class="breadcrumb-item active">Consulta de Stock</li>
    </ol>
    <div class="input-group">
        <asp:TextBox CssClass="form-control" ID="txtCodigo" placeholder="Ingrese código a buscar..." runat="server"></asp:TextBox>
        <span class="input-group-append">
            <asp:Button CssClass="btn btn-primary" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
        </span>
    </div>
    <div class="card mb-3">
        <div class="card-header">
            <i class="fa fa-search-plus"></i>Consulta
        </div>
        <div class="card-body">
            <div class="table-bordered">
                <asp:GridView CssClass="table table-hover" ID="gvInformacion" runat="server" Width="100%" Visible="False" AutoGenerateColumns="False" DataSourceID="odsCentroCostos" OnSelectedIndexChanging="gvInformacion_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" />
                        <asp:BoundField DataField="CentroCosto" HeaderText="CentroCosto" SortExpression="CentroCosto" />
                        <asp:BoundField DataField="Stocks" HeaderText="Stocks" SortExpression="Stocks" />
                        <asp:CommandField SelectText="Ubicaciones" ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
                <hr />
                <asp:Label ID="lblUbicaciones" runat="server" Text="Ubicaciones" Visible="false"></asp:Label>
                <asp:GridView CssClass="table table-hover" ID="gvUbicaciones" runat="server" Visible="False" AutoGenerateColumns="False" DataSourceID="odsUbicacion">
                    <Columns>
                        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" SortExpression="Ubicacion" />
                        <asp:BoundField DataField="Stocks" HeaderText="Stocks" SortExpression="Stocks" />
                    </Columns>
                </asp:GridView>
<%--                <hr />
                <asp:Label ID="lblVoluntario" runat="server" Text="Voluntario" Visible="false"></asp:Label>
                <asp:GridView CssClass="table table-hover" ID="GridViewVoluntario" runat="server" Visible="False"></asp:GridView>
                <hr />
                <asp:Label ID="lblInvoluntario" runat="server" Text="Involuntario" Visible="false"></asp:Label>
                <asp:GridView CssClass="table table-hover" ID="GridViewInvoluntario" runat="server" Visible="False"></asp:GridView>
                <asp:GridView CssClass="table table-hover" ID="gvCentrosCosto" runat="server"></asp:GridView>
                <asp:GridView CssClass="table table-hover" ID="gvUbicaciones" runat="server"></asp:GridView>--%>
            </div>
        </div>
    </div>
    <div class="alert">
        <asp:Label Visible="false" ID="lblMensaje" runat="server" Text="Label"><strong>Error!</strong> Codigo no encontrado en la base de datos</asp:Label>
    </div>
    <asp:ObjectDataSource ID="odsCentroCostos" runat="server" SelectMethod="StockProductoCentroCosto" TypeName="S_Negocio.ColeccionStockDescripcion">
        <SelectParameters>
            <asp:SessionParameter Name="codigo" SessionField="codigo" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsUbicacion" runat="server" SelectMethod="StockProductoUbicaciones" TypeName="S_Negocio.ColeccionStock">
        <SelectParameters>
            <asp:SessionParameter Name="codigo" SessionField="codigo" Type="String" />
            <asp:SessionParameter Name="centro_costo" SessionField="centrocosto" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
