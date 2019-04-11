<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra2.Master" AutoEventWireup="true" CodeBehind="RevisionDetalles.aspx.cs" Inherits="S_Presentacion.Cotizacion.RevisionDetalles" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<title>Revision Detalles</title>
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
			<a>Cotizacion</a>
		</li>
        <li class="breadcrumb-item">
            <a href="../Cotizacion/RevisionEncabezado.aspx">Revision Encabezado</a>
        </li>
		<li class="breadcrumb-item active">Revision Detalles</li>
	</ol>
	<!-- Example Bar Chart Card-->
	<div class="card mb-3">
		<div class="card-header">
			<i class="fa fa-reorder"></i>Detalles
		</div>
		<div class="card-body">
			<div class="table-responsive">
				<asp:GridView ID="dvDetalles" CssClass="table table-bordered" DataKeyNames="ID" runat="server" AutoGenerateColumns="False" DataSourceID="odsDetalle">
					<Columns>
						<asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true"/>
						<asp:BoundField DataField="Correlativo" HeaderText="Correlativo" SortExpression="Correlativo" ReadOnly="true"/>
						<asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="Codigo" ReadOnly="true"/>
						<asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Height="72px" Text='<%# Bind("Descripcion") %>' TextMode="MultiLine" Width="434px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
						<asp:BoundField DataField="PrecioUnitario" HeaderText="PrecioUnitario" SortExpression="PrecioUnitario" ReadOnly="true"/>
						<asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="Cantidad" ReadOnly="true"/>
						<asp:BoundField DataField="Subtotal" HeaderText="Subtotal" SortExpression="Subtotal" ReadOnly="true"/>
						<asp:CommandField ShowDeleteButton="False" ShowEditButton="True" EditText="Editar" />
					</Columns>
				</asp:GridView>
			</div>
		</div>
	</div>
	<asp:ObjectDataSource ID="odsDetalle" runat="server" DataObjectTypeName="S_Negocio.Detalle" DeleteMethod="Delete" SelectMethod="ListaDetalle" TypeName="S_Negocio.ColeccionDetalle" UpdateMethod="Update">
		<SelectParameters>
			<asp:SessionParameter Name="correlativo" SessionField="cotizacion" Type="String" />
		</SelectParameters>
	</asp:ObjectDataSource>
</asp:Content>
