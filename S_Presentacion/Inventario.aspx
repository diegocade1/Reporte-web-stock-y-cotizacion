<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="S_Presentacion.Inventario" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inventario</title>
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
        <li class="breadcrumb-item active">Inventario</li>
    </ol>
    <div class="input-group">
        <asp:TextBox CssClass="form-control" ID="txtDescripcion" placeholder="Ingrese filtro (ejemplo: etiqueta o etiqueta,semibrillo)" runat="server"></asp:TextBox>
        <span class="input-group-append">
            <asp:Button CssClass="btn btn-primary" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
            <asp:Button CssClass="btn btn-primary" Visible="false" ID="btnQuitar" runat="server" Text="Quitar Filtro" OnClick="btnQuitar_Click" TabIndex="1" />
        </span>
    </div>
    <asp:ObjectDataSource ID="odsProducto" runat="server" SelectMethod="ListaProductos" TypeName="S_Negocio.ColeccionProducto"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsFiltro" runat="server" SelectMethod="ListaBusquedaProductos" TypeName="S_Negocio.ColeccionProducto">
        <SelectParameters>
            <asp:SessionParameter Name="palabra" SessionField="filtroPalabra" Type="String" DefaultValue="" />
            <asp:SessionParameter DefaultValue="" Name="descripcion" SessionField="filtroDescripcion" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsSort" runat="server" SelectMethod="ListaProductosOrdenar" TypeName="S_Negocio.ColeccionProducto">
        <SelectParameters>
            <asp:SessionParameter Name="lista" SessionField="sortLista" Type="Object" />
            <asp:SessionParameter DefaultValue="" Name="palabra" SessionField="sortPalabra" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCentroCosto" runat="server" SelectMethod="ListaProductosCentroCosto" TypeName="S_Negocio.ColeccionCentroCosto"></asp:ObjectDataSource>
          <!-- Example DataTables Card-->
      <div class="card mb-3">
        <div class="card-header">
          <i class="fa fa-industry"></i> Inventario</div>
        <div class="card-body">
            <div class="col-md-6">
                <label for="ddlCentroCosto">Centro de Costos</label>
                <asp:DropDownList ID="ddlCentroCosto" runat="server" DataSourceID="odsCentroCosto" DataTextField="Descripcion" DataValueField="Descripcion" AppendDataBoundItems = "true" >
                    <asp:ListItem Selected = "True" Text = "Todo" Value = "Todo"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnCentroCosto" runat="server" Text="Filtrar" OnClick="btnCentroCosto_Click" Visible="False" TabIndex="2" />
            </div>
          <div class="table-responsive">
              <asp:GridView ID="dvProductos" CssClass="table table-bordered" runat="server" DataSourceID="odsProducto" Width="100%" AutoGenerateColumns="False" AllowSorting="True" OnSorting="dvProductos_Sorting" PageSize="100" OnSelectedIndexChanging="dvProductos_SelectedIndexChanging">
                  <Columns>
                      <asp:CommandField ShowSelectButton="True" />
                      <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                      <asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" />
                      <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                      <asp:BoundField DataField="Familia" HeaderText="Familia" SortExpression="Familia" />
                      <asp:BoundField DataField="Paquete" HeaderText="Paquete" SortExpression="Paquete" />
                      <asp:BoundField DataField="Umedida" HeaderText="Umedida" SortExpression="Umedida" />
                      <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock" DataFormatString="{0:N0}"/>
                  </Columns>
                  <SelectedRowStyle BorderStyle="Dashed" />
              </asp:GridView>
          </div>
        </div>
      </div>
</asp:Content>
