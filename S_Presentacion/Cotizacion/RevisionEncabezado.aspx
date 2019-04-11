<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra2.Master" AutoEventWireup="true" CodeBehind="RevisionEncabezado.aspx.cs" Inherits="S_Presentacion.Cotizacion.Revision" MaintainScrollPositionOnPostback="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Revision Encabezado</title>
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
        <li class="breadcrumb-item active">Revision Encabezados</li>
    </ol>
    <!-- Example Bar Chart Card-->
    <div class="card mb-3">
        <div class="card-header">
            <i class="fa fa-reorder"></i>Encabezado
        </div>
        <div class="container">
            <div class="form-group">
                <div class="form-row">
                    <div class="col-md-6">
                        <asp:Label ID="lblFiltro" runat="server" Text="Filtro:"></asp:Label>
                        <asp:DropDownList ID="ddlFiltroEstados" runat="server" DataSourceID="odsEstadosCotizacion" DataTextField="Descripcion" DataValueField="Descripcion"></asp:DropDownList>
                        <asp:Button CssClass="btn btn-dark" ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="gvCotizacion" DataKeyNames="Correlativo" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" DataSourceID="odsCotizaciones" Width="100%" OnRowCommand="gvCotizacion_RowCommand" OnRowEditing="gvCotizacion_RowEditing" OnRowCancelingEdit="gvCotizacion_RowCancelingEdit" OnRowUpdating="gvCotizacion_RowUpdating" >
                    <Columns >
                        <asp:BoundField DataField="Correlativo" HeaderText="Correlativo" SortExpression="Correlativo" ReadOnly="True"/>
                        <asp:BoundField DataField="Razon_social" HeaderText="Razon Social" SortExpression="Razon_social" ReadOnly="True"/>
                        <asp:BoundField DataField="Rut" HeaderText="Rut" SortExpression="Rut" ReadOnly="True" />
                        <asp:BoundField DataField="Contacto" HeaderText="Contacto" SortExpression="Contacto" ReadOnly="True"/>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" ReadOnly="True"/>
                        <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
                        <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                        <asp:BoundField DataField="CondicionPago" HeaderText="Condicion de Pago" SortExpression="CondicionPago" ReadOnly="True"/>
                        <asp:BoundField DataField="Entrega" HeaderText="Entrega" SortExpression="Entrega" ReadOnly="True"/>
                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" ReadOnly="True"/>
                        <asp:BoundField DataField="Tipo_moneda" HeaderText="Tipo de Moneda" SortExpression="Tipo_moneda" ReadOnly="True"/>
                        <asp:TemplateField HeaderText="Estado" SortExpression="Estado">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEstado" runat="server" DataSourceID="odsEstadosCotizacion" DataTextField="Descripcion" DataValueField="Descripcion" SelectedValue='<%# Bind("Estado") %>' AppendDataBoundItems="True">
                                    <asp:ListItem Selected="false" Text="--Seleccione un Estado" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Observacion_estado" SortExpression="Observacion_estado">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Height="117px" Text='<%# Bind("Observacion_estado") %>' TextMode="MultiLine" Width="336px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Observacion_estado") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Codigo_usuario" HeaderText="Codigo de Usuario" SortExpression="Codigo_usuario" ReadOnly="True"/>
                        <asp:BoundField DataField="Neto" HeaderText="Neto" SortExpression="Neto" ReadOnly="True"/>
                        <asp:BoundField DataField="Iva" HeaderText="Iva" SortExpression="Iva" ReadOnly="True"/>
                        <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" ReadOnly="True"/>
                        <asp:CommandField EditText="Editar" ShowEditButton="True" UpdateText="Modificar" />
						<asp:ButtonField CommandName="Details" Text="Detalles" />
						<asp:ButtonField CommandName="Cotizacion" Text="Editar Cotizacion" />
						<asp:ButtonField ButtonType="Button" CommandName="Select" Text="Imprimir PDF" />
                        <asp:ButtonField ButtonType="Button" CommandName="Excel" Text="Imprimir Excel" />
                    </Columns>
                </asp:GridView>
             </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsCotizaciones" runat="server" SelectMethod="ListaEncabezadoPendientes" TypeName="S_Negocio.ColeccionEncabezado" DataObjectTypeName="S_Negocio.Encabezado" DeleteMethod="Delete" UpdateMethod="Update"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsEstadosCotizacion" runat="server" SelectMethod="ListaDetalle" TypeName="S_Negocio.ColeccionEstadosCotizacion"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsEstadosFiltro" runat="server" SelectMethod="ListaEncabezadoFiltro" TypeName="S_Negocio.ColeccionEncabezado">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="pendiente" Name="filtro" SessionField="estado" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
