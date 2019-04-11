<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra.Master" AutoEventWireup="true" CodeBehind="Movimientos.aspx.cs" Inherits="S_Presentacion.Movimientos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Movimientos</title>
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
        <li class="breadcrumb-item active">Movimientos</li>
    </ol>
    <!-- Example Bar Chart Card-->
    <asp:ObjectDataSource ID="odsRegistro" runat="server" SelectMethod="Movimientos" TypeName="S_Negocio.ColeccionRegistro">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="fecha_ini" SessionField="fechaIni" Type="String" />
            <asp:SessionParameter DefaultValue="" Name="fecha_fin" SessionField="fechaFin" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="card mb-3">
        <div class="card-header">
            <i class="fa fa-table"></i>Movimientos
        </div>
        <div class="card-body">
            <div class="table-responsive">
                <table class="table table-light" runat="server">
                    <tr>
                        <th>Fecha Inicio:</th>
                        <th>Fecha Termino:</th>
                    </tr>
                    <tr>
                        <td><input type="date" ID="txtFechaIni" runat="server"></td>
                        <td><input type="date" ID="txtFechaFin" runat="server"></td>
                    </tr>
                </table>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" /><br />
                <asp:Label ID="lblMensaje" runat="server" Text="No se encontraron registros." Visible="False" ForeColor="Red"></asp:Label><br />
                <asp:ListView ID="lvRegistro" runat="server" Visible="False" DataSourceID="odsRegistro">
                    <AlternatingItemTemplate>
                        <tr style="background-color:#FFF8DC;">
                            <td>
                                <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>' />
                            </td>
                            <td>
                                <asp:Label ID="HoraLabel" runat="server" Text='<%# Eval("Hora") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FechaLabel" runat="server" Text='<%# Eval("Fecha") %>' />
                            </td>
                            <td>
                                <asp:Label ID="EgresoLabel" runat="server" Text='<%# Eval("Egreso") %>' />
                            </td>
                            <td>
                                <asp:Label ID="IngresoLabel" runat="server" Text='<%# Eval("Ingreso") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CodigoLabel" runat="server" Text='<%# Eval("Codigo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EditItemTemplate>
                        <tr style="background-color:#008A8C;color: #FFFFFF;">
                            <td>
                                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                            </td>
                            <td>
                                <asp:TextBox ID="DescripcionTextBox" runat="server" Text='<%# Bind("Descripcion") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="HoraTextBox" runat="server" Text='<%# Bind("Hora") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="FechaTextBox" runat="server" Text='<%# Bind("Fecha") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="EgresoTextBox" runat="server" Text='<%# Bind("Egreso") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="IngresoTextBox" runat="server" Text='<%# Bind("Ingreso") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="CodigoTextBox" runat="server" Text='<%# Bind("Codigo") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="IDTextBox" runat="server" Text='<%# Bind("ID") %>' />
                            </td>
                        </tr>
                    </EditItemTemplate>
                    <EmptyDataTemplate>
                        <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                            <tr>
                                <td>No data was returned.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <InsertItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                            </td>
                            <td>
                                <asp:TextBox ID="DescripcionTextBox" runat="server" Text='<%# Bind("Descripcion") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="HoraTextBox" runat="server" Text='<%# Bind("Hora") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="FechaTextBox" runat="server" Text='<%# Bind("Fecha") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="EgresoTextBox" runat="server" Text='<%# Bind("Egreso") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="IngresoTextBox" runat="server" Text='<%# Bind("Ingreso") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="CodigoTextBox" runat="server" Text='<%# Bind("Codigo") %>' />
                            </td>
                            <td>
                                <asp:TextBox ID="IDTextBox" runat="server" Text='<%# Bind("ID") %>' />
                            </td>
                        </tr>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <tr style="background-color:#DCDCDC;color: #000000;">
                            <td>
                                <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>' />
                            </td>
                            <td>
                                <asp:Label ID="HoraLabel" runat="server" Text='<%# Eval("Hora") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FechaLabel" runat="server" Text='<%# Eval("Fecha") %>' />
                            </td>
                            <td>
                                <asp:Label ID="EgresoLabel" runat="server" Text='<%# Eval("Egreso") %>' />
                            </td>
                            <td>
                                <asp:Label ID="IngresoLabel" runat="server" Text='<%# Eval("Ingreso") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CodigoLabel" runat="server" Text='<%# Eval("Codigo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                        <tr runat="server" style="background-color:#DCDCDC;color: #000000;">
                                            <th runat="server">Descripcion</th>
                                            <th runat="server">Hora</th>
                                            <th runat="server">Fecha</th>
                                            <th runat="server">Egreso</th>
                                            <th runat="server">Ingreso</th>
                                            <th runat="server">Codigo</th>
                                            <th runat="server">ID</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                    <asp:DataPager ID="DataPager1" runat="server">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                            <asp:NumericPagerField />
                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <SelectedItemTemplate>
                        <tr style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">
                            <td>
                                <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>' />
                            </td>
                            <td>
                                <asp:Label ID="HoraLabel" runat="server" Text='<%# Eval("Hora") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FechaLabel" runat="server" Text='<%# Eval("Fecha") %>' />
                            </td>
                            <td>
                                <asp:Label ID="EgresoLabel" runat="server" Text='<%# Eval("Egreso") %>' />
                            </td>
                            <td>
                                <asp:Label ID="IngresoLabel" runat="server" Text='<%# Eval("Ingreso") %>' />
                            </td>
                            <td>
                                <asp:Label ID="CodigoLabel" runat="server" Text='<%# Eval("Codigo") %>' />
                            </td>
                            <td>
                                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                            </td>
                        </tr>
                    </SelectedItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
    </asp:Content>
