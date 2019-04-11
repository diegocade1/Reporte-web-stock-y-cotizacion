<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra2.Master" AutoEventWireup="true" CodeBehind="EditorCotizacion.aspx.cs" Inherits="S_Presentacion.Cotizacion.EditorCotizacion" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>Editor de Cotizacion</title>
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
	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("click", ".btn-outline-primary", function () { //
                var tipo = $('.txtTipoCotizacion').val();
                var rowCount = $('.detalles-cotizacion').length + 1;
    //            if (rowCount <= 9)
				//{
					var contactdiv = '<tr class="detalles-cotizacion">' +
                        '<td><input class="form-control cantidad" value="0" name="txtCantidad' + rowCount + '" type="text" onKeyPress="return soloNumeroCantidad(event)" oninput="SubtotalFunction()" onchange="NetoFunction()"></td>' +
                        '<td><div class="tooltip-wrap"><input class="form-control" name="txtDescripcion' + rowCount + '" type="text" readonly><div class="tooltip-content"><p id="descripcionCompleta' + rowCount + '"></p>' +
						'</div></div><a class="nav-link" id="btnTipoStock' + rowCount + '" data-toggle="modal" data-target="#Modal' + tipo + '" onclick="ModalLleno(document.getElementById(' + "\'" + "btnTipoStock" + rowCount + '\').id)">' +
						'<i class="fa fa-fw fa-plus-square"></i>' +
						'</a>' +
						'</td > ' +
						'<td><input class="form-control precio" value="0" name="txtPrecioUnit' + rowCount + '" type="text" onKeyPress="return soloNumeroPrecio(event,this)" oninput="SubtotalFunction()" onchange="NetoFunction()"></td>' +
                        '<td><input class="form-control" name="txtCodigo' + rowCount + '" type="text" readonly>' +
                        '<a class="nav-link" id="btnCodigo' + rowCount + '" data-toggle="modal" data-target="#ModalCodigos"' + '" onclick="LineaIDCodigo(document.getElementById(' + "\'" + "btnCodigo" + rowCount + '\').id)">' +
                        '<i class="fa fa-fw fa-book"></i>'+
                        '</a>'+'</td>' +
						'<td><input class="form-control subtotal" value="0" name="txtSubTotal' + rowCount + '" type="text" readonly></td>' +
						'<td><button type="button" name="btnAñadir' + rowCount + '" class="btn-outline-primary">Añadir Campo</button>' +
						'<button type="button" name="btnEliminar' + rowCount + '" class="btn-outline-secondary">Eliminar Campo</button></td>' +
						'</tr>';
					$('#tablaFormulario').append(contactdiv); // Adding these controls to Main table class

    //            }
    //            else
				//{
				//	alert("Cantidad maxima de items alcanzada para la impresion del pdf.(9 items)");
    //            }
			});
        });

        $(document).on("click", ".btn-outline-secondary", function () {
            $(this).closest("tr").remove(); // closest used to remove the respective 'tr' in which I have my controls
            NetoFunction();
        });

        function soloNumeroPrecio(evt,obj) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function soloNumeroCantidad(evt) {
            
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
        function SubtotalFunction() {
                var tbl = document.getElementById("tablaFormulario");
                var filas = tbl.rows.length;
                var cantidad;
                var precio;
            var total = 0;
                for (var i = 0; i < filas - 1; i++) {
                    cantidad = document.getElementsByClassName("form-control cantidad")[i].value;
                    precio = document.getElementsByClassName("form-control precio")[i].value;
                    if (!parseInt(cantidad)) {
                        cantidad = 0;
                    }
                    else if (!parseFloat(precio)) {
                        precio = 0;
                    }
                    total = (parseInt(cantidad) * parseFloat(precio));
                    document.getElementsByClassName("form-control subtotal")[i].value = total.toFixed(2);
            }
        }

        function NetoFunction() {
            var tbl = document.getElementById("tablaFormulario");
            var filas = tbl.rows.length;
            var subtotal;
            var neto = 0;
            for (var i = 0; i < filas - 1; i++) {
                 //cantidad = document.getElementsByClassName("form-control cantidad")[i].value;
                 //precio = document.getElementsByClassName("form-control precio")[i].value;
                subtotal = document.getElementsByClassName("form-control subtotal")[i].value;
                //subtotal = (parseInt(cantidad) * parseInt(precio));
                neto += parseFloat(subtotal);
                document.getElementsByClassName("form-control neto")[0].value = neto.toFixed(2);
                //IvaFunction();
                TotalFunction();
            }
        }

        function IvaFunction() {
            var neto = document.getElementsByClassName("form-control neto")[0].value;
            var iva =  parseInt(neto) * 0.19;
            document.getElementsByClassName("form-control iva")[0].value = Math.round(iva);
            TotalFunction();
        }

        function TotalFunction() {
            //var neto = document.getElementsByClassName("form-control neto")[0].value;
            //var iva = document.getElementsByClassName("form-control iva")[0].value;
            //var total = parseInt(neto) + parseInt(iva)
            var total = document.getElementsByClassName("form-control neto")[0].value;
            document.getElementsByClassName("form-control total")[0].value = total;
        }

        function Valida_Rut(Objeto) {
            var tmpstr = "";
            var intlargo = Objeto.value
            if (intlargo.length > 0) {
                crut = Objeto.value
                largo = crut.length;
                if (largo < 2) {
                    document.getElementById("mensaje").innerHTML = " El rut ingresado es invalido"
                    Objeto.focus()
                    return false;
                }
                for (i = 0; i < crut.length ; i++)
                    if (crut.charAt(i) != ' ' && crut.charAt(i) != '.' && crut.charAt(i) != '-') {
                        tmpstr = tmpstr + crut.charAt(i);
                    }
                rut = tmpstr;
                crut = tmpstr;
                largo = crut.length;
                if (largo > 2)
                    rut = crut.substring(0, largo - 1);
                else
                    rut = crut.charAt(0);
                    dv = crut.charAt(largo - 1);
                if (rut == null || dv == null)
                    return 0;
                var dvr = '0';
                suma = 0;
                mul = 2;
                for (i = rut.length - 1 ; i >= 0; i--) {
                    suma = suma + rut.charAt(i) * mul;
                    if (mul == 7)
                        mul = 2;
                    else
                        mul++;
                }
                res = suma % 11;
                if (res == 1)
                    dvr = 'k';
                else if (res == 0)
                    dvr = '0';
                else {
                    dvi = 11 - res;
                    dvr = dvi + "";
                }
                if (dvr != dv.toLowerCase()) {
                    document.getElementById("mensaje").innerHTML = " El rut ingresado es invalido"
                    Objeto.focus()
                    return false;
                }
                document.getElementById("mensaje").innerHTML = ""
                return true;
            }
        }

        function tipoStock(objeto) {
			var rowCount = $('.detalles-cotizacion').length;
			for (var i = 1; i < (rowCount + 1); i++) {
				var button = document.getElementById('btnTipoStock' + i);
				if (objeto.value == "Etiqueta") {
					button.setAttribute("data-target", "#ModalEtiqueta");
            }
				else if (objeto.value == "Cinta") {
					button.setAttribute("data-target", "#ModalCinta");
            }
				else if (objeto.value == "Hardware") {
					button.setAttribute("data-target", "#ModalHardware");
                }
                else if (objeto.value == "Otros") {
                    button.setAttribute("data-target", "#ModalOtros");
                }
			}
        }

        function LineaEtiqueta(id) {
            var contenido = $("#" + id).val();
            //alert(contenido);
            //if (contenido == "") {
            //    var rowCount = $('.detalles-cotizacion').length;
            //    var ancho = $("#txtAnchoEtiqueta").val();
            //    var avance = $("#txtAvanceEtiqueta").val();
            //    var aro = $("#txtAroEtiqueta").val();
            //    var material = $("#txtMaterialEtiqueta").val();
            //    var etqXrollo = $("#txtEtiqRolloEtiqueta").val();
            //    var colores = $("#txtColoresEtiqueta").val();
            //    var observacion = $("#txtObservacionEtiqueta").val();
            //    var salida = $("#txtSalidaEtiqueta").val();
            //    var codigo = $("#txtCodigoEtiqueta").val();
            //    var linea = "Etiqueta Ancho " + ancho + " Avance " + avance + " Material " + material + " Aro " + aro + " Etiquetas por Rollo " + etqXrollo + " Colores " + colores + " Salida " + salida + " Observacion " + observacion;
            //    $("#txtLinea").val(linea);
            //    $("input[name=txtCodigo" + rowCount + "]").val(codigo);
            //    $("input[name=txtDescripcion" + rowCount + "]").val($("#txtLinea").val());
            //    //alert(linea + " row " + rowCount + "id vacio" + id.value);
            //    //PageMethods.NumPagination($(this).attr('id'));
            //    $("#ModalEtiqueta").modal("hide");

            //}
            //else {
                var rowCount = contenido;
                var ancho = $("#txtAnchoEtiqueta").val();
                var avance = $("#txtAvanceEtiqueta").val();
                var aro = $("#txtAroEtiqueta").val();
                var material = $("#txtMaterialEtiqueta").val();
                var etqXrollo = $("#txtEtiqRolloEtiqueta").val();
                var colores = $("#txtColoresEtiqueta").val();
                var observacion = $("#txtObservacionEtiqueta").val();
                var salida = $("#txtSalidaEtiqueta").val();
                var codigo = $("#txtCodigoEtiqueta").val();
                var linea = "Etiqueta Ancho " + ancho + " Avance " + avance + " Material " + material + " Aro " + aro + " Etiq por Rollo " + etqXrollo + " Colores " + colores + " Salida " + salida + " Observacion " + observacion;
                $("#descripcionCompleta"+rowCount).text(linea);
                $("#txtLinea").val(linea);
                $("input[name=txtCodigo" + rowCount + "]").val(codigo);
                $("input[name=txtDescripcion" + rowCount + "]").val($("#txtLinea").val());
                //PageMethods.NumPagination($(this).attr('id'));
                //alert(linea + " row " + rowCount + "id no vacio");
            $("#" + id).val("");
            $("#txtAnchoEtiqueta").val("");
            $("#txtAvanceEtiqueta").val("");
            $("#txtAroEtiqueta").val("");
            $("#txtMaterialEtiqueta").val("");
            $("#txtEtiqRolloEtiqueta").val("");
            $("#txtColoresEtiqueta").val("");
            $("#txtObservacionEtiqueta").val("");
            $("#txtCodigoEtiqueta").val("");
            $("#txtSalidaEtiqueta").val("");
                $("#ModalEtiqueta").modal("hide");
            //}

        }

        function LineaCinta(id) {
            var contenido = $("#" + id).val();
            //alert(contenido);
            //if (contenido == "") {
            //    var rowCount = $('.detalles-cotizacion').length;
            //    var ancho = $("#txtAnchoEtiqueta").val();
            //    var avance = $("#txtAvanceEtiqueta").val();
            //    var aro = $("#txtAroEtiqueta").val();
            //    var material = $("#txtMaterialEtiqueta").val();
            //    var etqXrollo = $("#txtEtiqRolloEtiqueta").val();
            //    var colores = $("#txtColoresEtiqueta").val();
            //    var observacion = $("#txtObservacionEtiqueta").val();
            //    var salida = $("#txtSalidaEtiqueta").val();
            //    var codigo = $("#txtCodigoEtiqueta").val();
            //    var linea = "Etiqueta Ancho " + ancho + " Avance " + avance + " Material " + material + " Aro " + aro + " Etiquetas por Rollo " + etqXrollo + " Colores " + colores + " Salida " + salida + " Observacion " + observacion;
            //    $("#txtLinea").val(linea);
            //    $("input[name=txtCodigo" + rowCount + "]").val(codigo);
            //    $("input[name=txtDescripcion" + rowCount + "]").val($("#txtLinea").val());
            //    //alert(linea + " row " + rowCount + "id vacio" + id.value);
            //    //PageMethods.NumPagination($(this).attr('id'));
            //    $("#ModalEtiqueta").modal("hide");

            //}
            //else {
                var rowCount = contenido;
                var ancho = $("#txtAnchoCinta").val();
                var largo = $("#txtLargoCinta").val();
                var aro = $("#txtAroCinta").val();
                var material = $("#txtMaterialCinta").val();
                var observacion = $("#txtObservacionCinta").val();
                var codigo = $("#txtCodigoCinta").val();
                var linea = "Cinta Ancho " + ancho + " Largo " + largo + " Material " + material + " Aro " + aro + " Observacion " + observacion;
                $("#descripcionCompleta"+rowCount).text(linea);
                $("#txtLinea").val(linea);
                $("input[name=txtCodigo" + rowCount + "]").val(codigo);
                $("input[name=txtDescripcion" + rowCount + "]").val($("#txtLinea").val());
                //alert(linea + " row " + rowCount + "id no vacio");
            $("#" + id).val("");
            $("#txtAnchoCinta").val("");
            $("#txtLargoCinta").val("");
            $("#txtAroCinta").val("");
            $("#txtMaterialCinta").val("");
            $("#txtObservacionCinta").val("");
            $("#txtCodigoCinta").val("");
                $("#ModalCinta").modal("hide");
            //}
        }

        function LineaHardware(id) {
            var contenido = $("#" + id).val();
            //alert(contenido);
            //if (contenido == "") {
            //    var rowCount = $('.detalles-cotizacion').length;
            //    var ancho = $("#txtAnchoEtiqueta").val();
            //    var avance = $("#txtAvanceEtiqueta").val();
            //    var aro = $("#txtAroEtiqueta").val();
            //    var material = $("#txtMaterialEtiqueta").val();
            //    var etqXrollo = $("#txtEtiqRolloEtiqueta").val();
            //    var colores = $("#txtColoresEtiqueta").val();
            //    var observacion = $("#txtObservacionEtiqueta").val();
            //    var salida = $("#txtSalidaEtiqueta").val();
            //    var codigo = $("#txtCodigoEtiqueta").val();
            //    var linea = "Etiqueta Ancho " + ancho + " Avance " + avance + " Material " + material + " Aro " + aro + " Etiquetas por Rollo " + etqXrollo + " Colores " + colores + " Salida " + salida + " Observacion " + observacion;
            //    $("#txtLinea").val(linea);
            //    $("input[name=txtCodigo" + rowCount + "]").val(codigo);
            //    $("input[name=txtDescripcion" + rowCount + "]").val($("#txtLinea").val());
            //    //alert(linea + " row " + rowCount + "id vacio" + id.value);
            //    //PageMethods.NumPagination($(this).attr('id'));
            //    $("#ModalEtiqueta").modal("hide");

            //}
            //else {
                var rowCount = contenido;
                var marca = $("#txtMarcaHardware").val();
                var modelo = $("#txtModeloHardware").val();
                var familia= $("#txtFamiliaHardware").val();
                var observacion = $("#txtObservacionHardware").val();
                var codigo = $("#txtCodigoHardware").val();
                var linea = "Hardware Marca " + marca + " Modelo " + modelo + " Familia " + familia + " Observacion " + observacion;
                $("#descripcionCompleta"+rowCount).text(linea);
                $("#txtLinea").val(linea);
                $("input[name=txtCodigo" + rowCount + "]").val(codigo);
                $("input[name=txtDescripcion" + rowCount + "]").val($("#txtLinea").val());
                //PageMethods.NumPagination($(this).attr('id'));
                //alert(linea + " row " + rowCount + "id no vacio");
            $("#" + id).val("");
            $("#txtMarcaHardware").val("");
            $("#txtModeloHardware").val("");
            $("#txtFamiliaHardware").val("");
            $("#txtObservacionHardware").val("");
            $("#txtCodigoHardware").val("");
                $("#ModalHardware").modal("hide");
            //}

        }

        function LineaOtros(id) {
            var contenido = $("#" + id).val();
            //alert(contenido);
            //if (contenido == "") {
            //    var rowCount = $('.detalles-cotizacion').length;
            //    var ancho = $("#txtAnchoEtiqueta").val();
            //    var avance = $("#txtAvanceEtiqueta").val();
            //    var aro = $("#txtAroEtiqueta").val();
            //    var material = $("#txtMaterialEtiqueta").val();
            //    var etqXrollo = $("#txtEtiqRolloEtiqueta").val();
            //    var colores = $("#txtColoresEtiqueta").val();
            //    var observacion = $("#txtObservacionEtiqueta").val();
            //    var salida = $("#txtSalidaEtiqueta").val();
            //    var codigo = $("#txtCodigoEtiqueta").val();
            //    var linea = "Etiqueta Ancho " + ancho + " Avance " + avance + " Material " + material + " Aro " + aro + " Etiquetas por Rollo " + etqXrollo + " Colores " + colores + " Salida " + salida + " Observacion " + observacion;
            //    $("#txtLinea").val(linea);
            //    $("input[name=txtCodigo" + rowCount + "]").val(codigo);
            //    $("input[name=txtDescripcion" + rowCount + "]").val($("#txtLinea").val());
            //    //alert(linea + " row " + rowCount + "id vacio" + id.value);
            //    //PageMethods.NumPagination($(this).attr('id'));
            //    $("#ModalEtiqueta").modal("hide");

            //}
            //else {
            var rowCount = contenido;
            var descripcion = $("#txtDescripcionOtros").val();
            var linea = descripcion;
            $("#descripcionCompleta"+rowCount).text(linea);
                $("#txtLinea").val(linea);
                //$("input[name=txtCodigo" + rowCount + "]").val(codigo);
                $("input[name=txtDescripcion" + rowCount + "]").val($("#txtLinea").val());
                //PageMethods.NumPagination($(this).attr('id'));
                //alert(linea + " row " + rowCount + "id no vacio");
            $("#" + id).val("");
            $("#txtDescripcionOtros").val("");
            //$("#txtCodigoHardware").val("");
                $("#ModalOtros").modal("hide");
            //}

        }

        function ModalLleno(id) {
            var rowCount = id.replace("btnTipoStock", "");
            $("#txtLineaEtiqueta").val(rowCount);
            $("#txtLineaCinta").val(rowCount);
            $("#txtLineaHardware").val(rowCount);
            $("#txtLineaOtros").val(rowCount);
            var descripcion = $("input[name=txtDescripcion" + rowCount + "]").val();
            var codigo = $("input[name=txtCodigo" + rowCount + "]").val();
            var linea;
            if (descripcion != "") {
                if (descripcion.includes("Etiqueta")) {
                var i = 0, strLength = descripcion.length;
                //for (i; i < strLength; i++) {
                //    descripcion = descripcion.replace(" ", "");
                //}
                //descripcion = descripcion.replace(" ", "");
                linea = descripcion.replace("Etiqueta ", "");
                linea = linea.replace("Ancho ", "");
                linea = linea.replace("Avance ", ";");
                linea = linea.replace("Material ", ";");
                linea = linea.replace("Aro ", ";");
                linea = linea.replace("Etiq por Rollo ", ";");
                linea = linea.replace("Colores ", ";");
                linea = linea.replace("Salida ", ";");
                linea = linea.replace("Observacion ", ";") + ";" + codigo;
                var array = linea.split(';');
                $("#txtAnchoEtiqueta").val(array[0]);
                $("#txtAvanceEtiqueta").val(array[1]);
                $("#txtMaterialEtiqueta").val(array[2]);
                $("#txtAroEtiqueta").val(array[3]);
                $("#txtEtiqRolloEtiqueta").val(array[4]);
                $("#txtColoresEtiqueta").val(array[5]);
                $("#txtSalidaEtiqueta").val(array[6]);
                $("#txtObservacionEtiqueta").val(array[7]);
                $("#txtCodigoEtiqueta").val(array[8]);
                //alert(linea + " row " + rowCount + " txtlinea " +$("#txtLineaEtiqueta").val());
                }
                else if (descripcion.includes("Cinta")) {
                var i = 0, strLength = descripcion.length;
                //for (i; i < strLength; i++) {
                //    descripcion = descripcion.replace(" ", "");
                //}
                //descripcion = descripcion.replace(" ", "");
                linea = descripcion.replace("Cinta ", "");
                linea = linea.replace("Ancho ", "");
                linea = linea.replace("Largo ", ";");
                linea = linea.replace("Material ", ";");
                linea = linea.replace("Aro ", ";");
                linea = linea.replace("Observacion ", ";") + ";" + codigo;
                var array = linea.split(';');
                $("#txtAnchoCinta").val(array[0]);
                $("#txtLargoCinta").val(array[1]);
                $("#txtMaterialCinta").val(array[2]);
                $("#txtAroCinta").val(array[3]);
                $("#txtObservacionCinta").val(array[4]);
                $("#txtCodigoCinta").val(array[5]);
                //alert(linea + " row " + rowCount + " txtlinea " +$("#txtLineaEtiqueta").val());
                }
                else if (descripcion.includes("Hardware")) {
                var i = 0, strLength = descripcion.length;
                //for (i; i < strLength; i++) {
                //    descripcion = descripcion.replace(" ", "");
                //}
                //descripcion = descripcion.replace(" ", "");
                linea = descripcion.replace("Hardware ", "");
                linea = linea.replace("Marca ", "");
                linea = linea.replace("Modelo ", ";");
                linea = linea.replace("Familia ", ";");
                linea = linea.replace("Observacion ", ";") + ";" + codigo;
                var array = linea.split(';');
                $("#txtMarcaHardware").val(array[0]);
                $("#txtModeloHardware").val(array[1]);
                $("#txtFamiliaHardware").val(array[2]);
                $("#txtObservacionHardware").val(array[3]);
                $("#txtCodigoHardware").val(array[4]);
                //alert(linea + " row " + rowCount + " txtlinea " +$("#txtLineaEtiqueta").val());
                }
                else {
                var i = 0, strLength = descripcion.length;
                //for (i; i < strLength; i++) {
                //    descripcion = descripcion.replace(" ", "");
                //}
                //descripcion = descripcion.replace(" ", "");
                //linea = descripcion.replace("Hardware ", "");
                //linea = linea.replace("Marca ", "");
                //linea = linea.replace("Modelo ", ";");
                //linea = linea.replace("Familia ", ";");
                //linea = linea.replace("Observacion ", ";") + ";" + codigo;
                //var array = linea.split(';');
                $("#txtDescripcionOtros").val(descripcion);
                //$("#txtModeloHardware").val(array[1]);
                //$("#txtFamiliaHardware").val(array[2]);
                //$("#txtObservacionHardware").val(array[3]);
                //$("#txtCodigoHardware").val(array[4]);
                //alert(linea + " row " + rowCount + " txtlinea " +$("#txtLineaEtiqueta").val());
                }
            }
		}
		function LimpiarEtiqueta() {
			$("#txtAnchoEtiqueta").val("");
			$("#txtAvanceEtiqueta").val("");
			$("#txtMaterialEtiqueta").val("");
			$("#txtAroEtiqueta").val("");
			$("#txtEtiqRolloEtiqueta").val("");
			$("#txtColoresEtiqueta").val("");
			$("#txtSalidaEtiqueta").val("");
			$("#txtObservacionEtiqueta").val("");
			$("#txtCodigoEtiqueta").val("");
			$("#ModalEtiqueta").modal("hide");
		}

		function LimpiarCinta() {
			$("#txtAnchoCinta").val("");
			$("#txtLargoCinta").val("");
			$("#txtMaterialCinta").val("");
			$("#txtAroCinta").val("");
			$("#txtObservacionCinta").val("");
			$("#txtCodigoCinta").val("");
			$("#ModalCinta").modal("hide");
		}
		function LimpiarHardware() {
			$("#txtMarcaHardware").val("");
			$("#txtModeloHardware").val("");
			$("#txtFamiliaHardware").val("");
			$("#txtObservacionHardware").val("");
			$("#txtCodigoHardware").val("");
			$("#ModalHardware").modal("hide");
        }

        function LimpiarOtros() {
			$("#txtDescripcionOtros").val("");
			$("#ModalOtros").modal("hide");
		}
        //$(document).ready(function () {
        //    var rowCount = $('.detalles-cotizacion').length;
        //    $(document).on('click', '[name=btnTipoStock' + rowCount + ']', function () {
        //        var texto = $("input[name=txtDescripcion" + rowCount + "]").val();
        //        if (texto != "") {
        //            alert("Prueba");
        //        }
        //    });
        //});

        function LineaIDCodigo(id) {
            var rowCount = id.replace("btnCodigo", "");
            $("#txtLineaCodigo").val(rowCount);
		}

        function LineaCodigo(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            //var grvCodigos = document.getElementById('gvCodigos');
            var grvCodigos = document.getElementById('<%= gvCodigos.ClientID %>')
            var ri = parseInt(rowIndex);
            //alert(grvCodigos);
            //var codigo = grvCodigos.rows[ri].cells[1].children[0];
            //var descripcion = grvCodigos.rows[ri].cells[2].children[0];        
            var SecondCellValue = grvCodigos.rows[ri].cells[1].innerHTML;
            var ThirdCellValue = grvCodigos.rows[ri].cells[2].innerHTML;
            var id = $("#txtLineaCodigo").val();
            //alert(SecondCellValue + " " + ThirdCellValue);
            $("input[name=txtCodigo" + id + "]").val(SecondCellValue);
            $("input[name=txtDescripcion" + id + "]").val(ThirdCellValue);
            $("#descripcionCompleta"+id).text(ThirdCellValue);
            $("#ModalCodigos").modal("hide");
        }

        $(document).ready(function () {
            document.getElementById("tablaFormulario").deleteRow(1);
            var listastring = document.getElementById('<%= objetoDetalles.ClientID %>').value;
            var listArray = listastring.split('~');
            for (var i = 0; i < listastring.length ; i++)
            {
                var array = listArray[i].split(';');
                var tipo = $('.txtTipoCotizacion').val();
                var rowCount = $('.detalles-cotizacion').length + 1;
                //if (rowCount <= 9)
                //{
                    var contactdiv = '<tr class="detalles-cotizacion">' +
                        '<td><input class="form-control cantidad" value="' + array[0] + '" name="txtCantidad' + rowCount + '" type="text" onKeyPress="return soloNumeroCantidad(event)" oninput="SubtotalFunction()" onchange="NetoFunction()"></td>' +
                        '<td><div class="tooltip-wrap"><input class="form-control" value="' + array[1] + '" name="txtDescripcion' + rowCount + '" type="text" readonly><div class="tooltip-content"><p id="descripcionCompleta' + rowCount + '"></p>' +
                        '</div></div><a class="nav-link" id="btnTipoStock' + rowCount + '" data-toggle="modal" data-target="#Modal' + tipo + '" onclick="ModalLleno(document.getElementById(' + "\'" + "btnTipoStock" + rowCount + '\').id)">' +
                        '<i class="fa fa-fw fa-plus-square"></i>' +
                        '</a>' +
                        '</td > ' +
                        '<td><input class="form-control precio" value="' + array[2] + '" name="txtPrecioUnit' + rowCount + '" type="text" onKeyPress="return soloNumeroPrecio(event,this)" oninput="SubtotalFunction()" onchange="NetoFunction()"></td>' +
                        '<td><input class="form-control" value="' + array[3] + '" name="txtCodigo' + rowCount + '" type="text" readonly>' +
                        '<a class="nav-link" id="btnCodigo' + rowCount + '" data-toggle="modal" data-target="#ModalCodigos"' + '" onclick="LineaIDCodigo(document.getElementById(' + "\'" + "btnCodigo" + rowCount + '\').id)">' +
                        '<i class="fa fa-fw fa-book"></i>' +
                        '</a>' + '</td>' +
                        '<td><input class="form-control subtotal" value="' + array[4] + '" name="txtSubTotal' + rowCount + '" type="text" readonly></td>' +
                        '<td><button type="button" name="btnAñadir' + rowCount + '" class="btn-outline-primary">Añadir Campo</button>';
                    if (rowCount != 1)
                    {
                            contactdiv += '<button type="button" name="btnEliminar' + rowCount + '" class="btn-outline-secondary">Eliminar Campo</button></td>'
                    }
                contactdiv = contactdiv + '</tr>';               
                    $('#tablaFormulario').append(contactdiv); // Adding these controls to Main table class
                //}
                $("#descripcionCompleta"+rowCount).text(array[1]);
            }
            //var tipo = $('.txtTipoCotizacion').val();
            //var rowCount = $('.detalles-cotizacion').length + 1;
            //if (rowCount <= 9)
            //{
            //    var contactdiv = '<tr class="detalles-cotizacion">' +
            //        '<td><input class="form-control cantidad" value="'+array[0]+'" name="txtCantidad' + rowCount + '" type="text" onKeyPress="return soloNumeroCantidad(event)" oninput="SubtotalFunction()" onchange="NetoFunction()"></td>' +
            //        '<td><input class="form-control" value="'+array[1]+'" name="txtDescripcion' + rowCount + '" type="text" readonly>' +
            //        '<a class="nav-link" id="btnTipoStock' + rowCount + '" data-toggle="modal" data-target="#Modal' + tipo + '" onclick="ModalLleno(document.getElementById(' + "\'" + "btnTipoStock" + rowCount + '\').id)">' +
            //        '<i class="fa fa-fw fa-plus-square"></i>' +
            //        '</a>' +
            //        '</td > ' +
            //        '<td><input class="form-control precio" value="'+array[2]+'" name="txtPrecioUnit' + rowCount + '" type="text" onKeyPress="return soloNumeroPrecio(event,this)" oninput="SubtotalFunction()" onchange="NetoFunction()"></td>' +
            //        '<td><input class="form-control" value="'+array[3]+'" name="txtCodigo' + rowCount + '" type="text" readonly>' +
            //        '<a class="nav-link" id="btnCodigo' + rowCount + '" data-toggle="modal" data-target="#ModalCodigos"' + '" onclick="LineaIDCodigo(document.getElementById(' + "\'" + "btnCodigo" + rowCount + '\').id)">' +
            //        '<i class="fa fa-fw fa-book"></i>' +
            //        '</a>' + '</td>' +
            //        '<td><input class="form-control subtotal" value="'+array[4]+'" name="txtSubTotal' + rowCount + '" type="text" readonly></td>' +
            //        '<td><button type="button" name="btnAñadir' + rowCount + '" class="btn-outline-primary">Añadir Campo</button>' +
            //        '<button type="button" name="btnEliminar' + rowCount + '" class="btn-outline-secondary">Eliminar Campo</button></td>' +
            //        '</tr>';
            //    $('#tablaFormulario').append(contactdiv); // Adding these controls to Main table class
            //    }
            //alert(linea);

        })

                function LineaCliente(row) {
            var rowData = row.parentNode.parentNode;
            var rowIndex = rowData.rowIndex;
            //var grvCodigos = document.getElementById('gvCodigos');
            var grvClientes = document.getElementById('<%= gvClientes.ClientID %>');
            var ri = parseInt(rowIndex);
            //alert(grvClientes);
            //var codigo = grvCodigos.rows[ri].cells[1].children[0];
            //var descripcion = grvCodigos.rows[ri].cells[2].children[0];        
            var SecondCellValue = grvClientes.rows[ri].cells[1].innerHTML;
            var ThirdCellValue = grvClientes.rows[ri].cells[2].innerHTML;
            var ForthCellValue = grvClientes.rows[ri].cells[3].innerHTML;
            var FifthCellValue = grvClientes.rows[ri].cells[4].innerHTML;
            var SixthCellValue = grvClientes.rows[ri].cells[5].innerHTML;
            var SeventhCellValue = grvClientes.rows[ri].cells[6].innerHTML;
            //var id = $("#txtLineaCliente").val();
            //alert(SecondCellValue + " " + ThirdCellValue + " " + ForthCellValue + " " + FifthCellValue + " " + SixthCellValue + " " + SeventhCellValue);
            $('#<%= txtNombre.ClientID %>').val(SecondCellValue);
            $('#<%= txtRut.ClientID %>').val(ThirdCellValue);
            $('#<%= txtContacto.ClientID %>').val(ForthCellValue);
            $('#<%= txtTelefono.ClientID %>').val(FifthCellValue);
            $('#<%= txtDireccion.ClientID %>').val(SixthCellValue);
            $('#<%= txtCorreo.ClientID %>').val(SeventhCellValue);

            $("#ModalClientes").modal("hide");
        }
    </script>
        <style>
.tooltip-wrap {
  position: relative;
}
.tooltip-wrap .tooltip-content {
  display: none;
  word-wrap: break-word;
  position: absolute;
  bottom: 5%;
  left: -15%;
  right: 5%;
  width:300px;
  background-color: #fff;
  padding: .5em;
}
.tooltip-wrap:hover .tooltip-content {
  display: block;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- html de sesion -->
    <%--<asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>--%>
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
	<asp:HiddenField ID="objetoDetalles" runat="server" />
     <ol class="breadcrumb">
        <li class="breadcrumb-item">
            <a href="../Inicio.aspx">Inicio</a>
        </li>
        <li class="breadcrumb-item">
            <a>Cotizacion</a>
        </li>
        <li class="breadcrumb-item active">Editor de Cotizacion</li>
    </ol>
    <!-- Example Bar Chart Card-->
    <div id="dvText" runat="server">
    <div class="container">
        <div class="card card-register mx-auto mt-5">
            <div class="card-header">Editor de Cotizacion</div>
            <div class="card-body">
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <label for="txtNombre">Razon Social</label>
                            <input class="form-control" id="txtNombre" type="text" aria-describedby="nameHelp" runat="server" placeholder="Ingrese Razon Social" maxlength="70" required>
                            <a class="nav-link" id="btnClientes" data-toggle="modal" data-target="#ModalClientes">
                                <i class="fa fa-fw fa-address-book"></i>
                            </a>
                        </div>
                        <div class="col-md-6">
                            <label for="txtRut">Rut&nbsp;</label><label style="color:red" for="exampleInputLastName" id="mensaje"></label>
                            <input class="form-control" id="txtRut" type="text" aria-describedby="nameHelp" runat="server" oninput="Valida_Rut(this)" placeholder="Ingrese Rut" maxlength="15" required>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <label for="txtContacto">Contacto</label>
                            <input class="form-control" id="txtContacto" type="text" aria-describedby="nameHelp" runat="server" placeholder="Ingrese Nombre de Contacto" maxlength="50" required>
                        </div>
                        <div class="col-md-6">
                            <label for="txtFecha_">Fecha</label>
                            <input class="form-control" id="txtFecha_" data-date="" data-date-format="DD MMMM YYYY" type="date" aria-describedby="nameHelp" runat="server" required>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtCorreo">Correo electronico</label>
                    <input class="form-control" id="txtCorreo" type="email" aria-describedby="emailHelp" runat="server" placeholder="Ingrese Correo Electronico" maxlength="100" required>
                </div>
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <label for="txtTelefono">Telefono</label>
                            <input class="form-control" id="txtTelefono" type="text" runat="server" placeholder="Ingrese Telefono" maxlength="20" required>
                        </div>
                        <div class="col-md-6">
                            <label for="exampleInputName">Condiciones de Pago</label>
                            <input class="form-control" id="txtCondicionesPago" type="text" runat="server" placeholder="Ingrese Condidicion de Pago" maxlength="50" required>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <label for="txtDireccion">Direccion</label>
                            <input class="form-control" id="txtDireccion" type="text" runat="server" placeholder="Ingrese Direccion" maxlength="100" required>
                        </div>
                        <div class="col-md-6">
                            <label for="txtEntrega">Entrega</label>
                            <input class="form-control" id="txtEntrega" type="text" runat="server" placeholder="Ingrese Tipo de Entrega" maxlength="50" required>
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
            </div>
        </div>
    </div>
    </div>
    <div class="card-body">
        <div class="table-bordered">
            <table class="table table-hover" id="tablaFormulario">
                <thead>
                    <tr>
                        <th>Cantidad</th>
                        <th>Descripcion <select id="txtTipoCotizacion" class="txtTipoCotizacion" runat="server" datasourceid="odsTipoCotizacion" datatextfield="descripcion" onchange="tipoStock(this)">
                                       </select>

                        </th>
                        <th>Precio Unit. <select id="txtMoneda" runat="server" datasourceid="odsMonedas" datatextfield="nombre" datavaluefield="id">
                                       </select>

                        </th>
                        <th>Codigo</th>
                        <th>Subtotal</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="detalles-cotizacion">
                        <td><input class="form-control cantidad" value="0" name="txtCantidad1" onKeyPress="return soloNumeroCantidad(event)" oninput="SubtotalFunction()" onchange="NetoFunction()" type="text" maxlength="11"></td>
                        <td>                           
                            <div class="tooltip-wrap">
                                <input class="form-control" name="txtDescripcion1" type="text" readonly>
                                <div class="tooltip-content">
                                    <p id="descripcionCompleta1">                                        
                                    </p>
                                </div>
                            </div>
                            <a class="nav-link" id="btnTipoStock1" data-toggle="modal" data-target="#ModalEtiqueta" onclick="ModalLleno(document.getElementById('btnTipoStock1').id)">
                                <i class="fa fa-fw fa-plus-square"></i>
                            </a>
                        </td>
                        <td><input class="form-control precio" value="0" name="txtPrecioUnit1" onKeyPress="return soloNumeroPrecio(event,this)" oninput="SubtotalFunction()" onchange="NetoFunction()" type="text" maxlength="14"/></td>
                        <td>
                            <input class="form-control" name="txtCodigo1" type="text" readonly />
                            <a class="nav-link" id="btnCodigo1" data-toggle="modal" data-target="#ModalCodigos" onclick="LineaIDCodigo(document.getElementById('btnCodigo1').id)">
                                <i class="fa fa-fw fa-book"></i>
                            </a>
                        </td>
                        <td><input class="form-control subtotal" value="0" name="txtSubTotal1" type="text" readonly/></td>
                        <td><button type="button" name="btnAñadir1" class="btn-outline-primary">Añadir Campo</button></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2" rowspan="3" ></td>
                        <td colspan="2">Neto</td>
                        <td><input class="form-control neto" value="0" id="txtNeto" type="text" readonly runat="server"/></td>
                        <td></td>
                    </tr>
                    <!--
                    <tr>
                        <td colspan="2">19% IVA</td>
                        <td><input class="form-control iva" value="0" id="txtIVA" type="text" readonly runat="server"/></td>
                        <td></td>
                    </tr>
                    -->
                    <tr>
                        <td colspan="2" rowspan="1">Total</td>
                        <td><input class="form-control total" value="0" id="txtTotal" type="text" readonly runat="server"/></td>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
		</div>
		<div style="width: 330px; margin: 0 auto;">
			<asp:Button CssClass="btn btn-dark" ID="btnEnviar" runat="server" Text="Modificar" OnClick="btnEnviar_Click" />
            <%--<a class="btn btn-dark" ID="btnLimpiar" href="../Cotizacion/Cotizar.aspx">Limpiar</a>--%>
		</div>
        <input type="hidden" id="txtLinea">
		<asp:ObjectDataSource ID="odsMonedas" runat="server" SelectMethod="ListaTipoMonedas" TypeName="S_Negocio.ColeccionTipoMoneda"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsTipoCotizacion" runat="server" SelectMethod="ListaTipoCotizaciones" TypeName="S_Negocio.ColeccionTipoCotizacion"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsCodigos" runat="server" SelectMethod="ListaCodigosProductos" TypeName="S_Negocio.ColeccionCodigosProductos"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsClientes" runat="server" SelectMethod="ListaEncabezadoClientes" TypeName="S_Negocio.ColeccionEncabezado"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsBusquedaCodigo" runat="server" SelectMethod="BusquedaGeneral" TypeName="S_Negocio.ColeccionCodigosProductos">
            <SelectParameters>
                <asp:SessionParameter Name="palabra" SessionField="palabra" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsBusquedaCliente" runat="server" SelectMethod="ListaEncabezadoClienteFiltro" TypeName="S_Negocio.ColeccionEncabezado">
            <SelectParameters>
                <asp:SessionParameter Name="filtro" SessionField="palabraCliente" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </div>
	<div class="modal" id="ModalEtiqueta" tabindex="-1" role="dialog" aria-labelledby="ModalEtiquetaLabel" aria-hidden="true">
		<div class="modal-dialog" role="button">
			<div class="modal-content">
                <input type="hidden" id="txtLineaEtiqueta">
				<div class="modal-header">
					<h6 class="modal-title" id="ModalEtiquetaLabel">Etiqueta</h6>
					<button class="close" type="button" data-dismiss="modal" aria-label="Close" onclick="LimpiarEtiqueta()">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body">
					<div class="container">
						<div class="card-body">
							<div class="form-group">
								<div class="form-row">
									<div class="col-md-6">
										<label for="txtAncho">Ancho(mm):</label>
										<input class="form-control" id="txtAnchoEtiqueta" type="text" aria-describedby="nameHelp" maxlength="5">
									</div>
									<div class="col-md-6">
										<label for="exampleInputLastName">Avance(mm):</label>
										<input class="form-control" id="txtAvanceEtiqueta" type="text" aria-describedby="nameHelp" maxlength="5">
									</div>
								</div>
							</div>
							<div class="form-group">
								<div class="form-row">
									<div class="col-md-6">
										<label for="txtAncho">Material:</label>
										<input class="form-control" id="txtMaterialEtiqueta" type="text" aria-describedby="nameHelp" maxlength="20">
									</div>
									<div class="col-md-6">
										<label for="exampleInputLastName">Aro:</label>
										<input class="form-control" id="txtAroEtiqueta" type="text" aria-describedby="nameHelp" maxlength="5">
									</div>
								</div>
							</div>
							<div class="form-group">
								<div class="form-row">
									<div class="col-md-6">
										<label for="txtAncho">Etiquetas/Rollo:</label>
										<input class="form-control" id="txtEtiqRolloEtiqueta" type="text" aria-describedby="nameHelp" maxlength="15">
									</div>
									<div class="col-md-6">
										<label for="exampleInputLastName">Colores:</label>
										<input class="form-control" id="txtColoresEtiqueta" type="text" aria-describedby="nameHelp" maxlength="20">
									</div>
								</div>
							</div>
							<div class="form-group">
								<div class="form-row">
									<div class="col-md-6">
										<label for="txtAncho">Salida:</label>
										<input class="form-control" id="txtSalidaEtiqueta" type="text" aria-describedby="nameHelp" maxlength="10">
									</div>
									<div class="col-md-6">
										<label for="exampleInputLastName">Observacion:</label>
										<input class="form-control" id="txtObservacionEtiqueta" type="text" aria-describedby="nameHelp" maxlength="136">
									</div>
								</div>
							</div>
                            <div class="form-group">
                                <div class="form-row">
                                    <div class="col-md-6">
                                        <label for="txtAncho">Codigo:</label>
                                        <input class="form-control" id="txtCodigoEtiqueta" type="text" aria-describedby="nameHelp" readonly>
                                    </div>
                                </div>
                            </div>
						</div>
					</div>
				</div>
				<div class="modal-footer">
					<div class="container">
						<div class="form-group">
							<div class="form-row">
								<div class="col-md-6">
									<button class="btn btn-dark" type="button" id="btnAceptarEtiqueta" onclick="LineaEtiqueta(document.getElementById('txtLineaEtiqueta').id)">Aceptar</button>
								</div>
								<div class="col-md-6">
									<button class="btn btn-dark" type="button" id="btnCancelarEtiqueta" onclick="LimpiarEtiqueta()">Cancelar</button>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
    	<div class="modal" id="ModalCinta" tabindex="-1" role="dialog" aria-labelledby="ModalCintaLabel" aria-hidden="true">
		<div class="modal-dialog" role="button">
			<div class="modal-content">
                <input type="hidden" id="txtLineaCinta">
                <div class="modal-header">
					<h6 class="modal-title" id="ModalCintaLabel">Cinta</h6>
					<button class="close" type="button" data-dismiss="modal" aria-label="Close" onclick="LimpiarCinta()">
						<span aria-hidden="true">×</span>
					</button>
				</div>
				<div class="modal-body">
					<div class="container">
						<div class="card-body">
							<div class="form-group">
								<div class="form-row">
									<div class="col-md-6">
										<label for="txtAncho">Ancho(mm):</label>
										<input class="form-control" id="txtAnchoCinta" type="text" aria-describedby="nameHelp" maxlength="5">
									</div>
									<div class="col-md-6">
										<label for="exampleInputLastName">Largo(mts):</label>
										<input class="form-control" id="txtLargoCinta" type="text" aria-describedby="nameHelp" maxlength="10">
									</div>
								</div>
							</div>
							<div class="form-group">
								<div class="form-row">
									<div class="col-md-6">
										<label for="txtAncho">Material:</label>
										<input class="form-control" id="txtMaterialCinta" type="text" aria-describedby="nameHelp" maxlength="20">
									</div>
									<div class="col-md-6">
										<label for="exampleInputLastName">Aro:</label>
										<input class="form-control" id="txtAroCinta" type="text" aria-describedby="nameHelp" maxlength="5">
									</div>
								</div>
							</div>
							<div class="form-group">
                                <div class="form-row">
                                    <div class="col-md-6">
                                        <label for="txtAncho">Codigo:</label>
                                        <input class="form-control" id="txtCodigoCinta" type="text" aria-describedby="nameHelp" readonly>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="txtAncho">Observacion:</label>
                                        <input class="form-control" id="txtObservacionCinta" type="text" aria-describedby="nameHelp" maxlength="213">
                                    </div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div class="modal-footer">
					<div class="container">
						<div class="form-group">
							<div class="form-row">
								<div class="col-md-6">
									<button class="btn btn-dark" type="button" id="btnAceptarCinta" onclick="LineaCinta(document.getElementById('txtLineaCinta').id)">Aceptar</button>
								</div>
								<div class="col-md-6">
									<button class="btn btn-dark" type="button" id="btnCancelarCinta" onclick="LimpiarCinta()">Cancelar</button>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
    <div class="modal" id="ModalHardware" tabindex="-1" role="dialog" aria-labelledby="ModalHardwareLabel" aria-hidden="true">
        <div class="modal-dialog" role="button">
            <div class="modal-content">
                <input type="hidden" id="txtLineaHardware">
                <div class="modal-header">
                    <h6 class="modal-title" id="ModalHardwareLabel">Hardware</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close" onclick="LimpiarHardware()">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="card-body">
                            <div class="form-group">
                                <div class="form-row">
                                    <div class="col-md-6">
                                        <label for="txtAncho">Marca:</label>
                                        <input class="form-control" id="txtMarcaHardware" type="text" aria-describedby="nameHelp" maxlength="25">
                                    </div>
                                    <div class="col-md-6">
                                        <label for="exampleInputLastName">Modelo:</label>
                                        <input class="form-control" id="txtModeloHardware" type="text" aria-describedby="nameHelp" maxlength="25">
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="form-row">
                                    <div class="col-md-6">
                                        <label for="txtAncho">Familia:</label>
                                        <input class="form-control" id="txtFamiliaHardware" type="text" aria-describedby="nameHelp" maxlength="20">
                                    </div>
                                    <div class="col-md-6">
                                        <label for="txtAncho">Codigo:</label>
                                        <input class="form-control" id="txtCodigoHardware" type="text" aria-describedby="nameHelp" readonly>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="form-row">
                                    <div class="col-md-6">
                                        <label for="exampleInputLastName">Observacion:</label>
                                        <input class="form-control" id="txtObservacionHardware" type="text" aria-describedby="nameHelp" maxlength="185">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="container">
                        <div class="form-group">
                            <div class="form-row">
                                <div class="col-md-6">
                                    <button class="btn btn-dark" type="button" id="btnAceptarHardware" onclick="LineaHardware(document.getElementById('txtLineaHardware').id)">Aceptar</button>
                                </div>
                                <div class="col-md-6">
                                    <button class="btn btn-dark" type="button" id="btnCancelarHardware" onclick="LimpiarHardware()">Cancelar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="ModalCodigos" tabindex="-1" role="dialog" aria-labelledby="ModalCodigosLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="button">
            <div class="modal-content">
                <input type="hidden" id="txtLineaCodigo">
                <%--<input type="text" id="txtLineaCodigo">--%>
                <div class="modal-header">
                    <h6 class="modal-title" id="ModalCodigoLabel">Codigos</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnQuitarBusqueda" EventName="Click" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvCodigos" EventName="PageIndexChanging" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="container">
                                <div class="card-body">
                                    <div class="form-group">
                                        <%--                                <div class="form-row">
                                    <div class="col-md-6">
                                        <label for="txtAncho">Buscar:</label>
                                        <input class="form-control" id="txtBuscarCodigo" type="text" aria-describedby="nameHelp">
                                    </div>
                                    <div class="col-md-6">
                                        <button class="btn btn-dark" type="button" id="btnBuscarCodigo">Buscar</button>
                                    </div>
                                </div>--%>
                                        <div class="form-row">
                                            <div class="col-md-6">
                                                <label for="txtAncho">Buscar:</label>
                                                <div class="input-group">
                                                    <input class="form-inline" runat="server" id="txtBuscarCodigo" type="text" aria-describedby="nameHelp" onkeypress="return event.keyCode != 13;"><br />
                                                    <%--<button class="btn btn-dark" type="button" id="btnBuscarCodigos" onclick="EjecutarBusqueda()">Buscar</button>--%>
                                                    <asp:LinkButton ID="btnBuscar" CssClass="btn btn-dark" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
                                                    <asp:LinkButton ID="btnQuitarBusqueda" CssClass="btn btn-dark" runat="server" OnClick="btnQuitarBusqueda_Click" Text="Quitar Filtro" Visible="false"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvCodigos" CssClass="table table-bordered" runat="server" Width="100%" DataSourceID="odsCodigos" AutoGenerateColumns="False" EnableSortingAndPagingCallbacks="false" OnPageIndexChanging="gvCodigos_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button class="btn btn-dark" type="button" id="btnseleccionarCodigo" onclick="LineaCodigo(this)">Seleccionar</button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Key" HeaderText="Codigo" ReadOnly="True" SortExpression="Key" />
                                    <asp:BoundField DataField="Value" HeaderText="Descripcion" ReadOnly="True" SortExpression="Value" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <div class="container">
                        <div class="form-group">
                            <div class="form-row">
                                <%--                                <div class="col-md-6">
                                    <button class="btn btn-dark" type="button" id="btnAceptarCodigo">Aceptar</button>
                                </div>--%>
                                <div class="col-md-6">
                                    <button class="btn btn-dark" id="btnCancelarCodigo" type="button" data-dismiss="modal">Cancelar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <div class="modal" id="ModalOtros" tabindex="-1" role="dialog" aria-labelledby="ModalOtrosLabel" aria-hidden="true">
        <div class="modal-dialog" role="button">
            <div class="modal-content">
                <input type="hidden" id="txtLineaOtros">
                <div class="modal-header">
                    <h6 class="modal-title" id="ModalOtrosLabel">Otros</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close" onclick="LimpiarHardware()">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="card-body">
                            <div class="form-group">
                                <div class="form-row">
                                    <div class="col-lg">
                                        <label for="txtAncho">Descripcion:</label>
                                        <%--<input class="form-control-file" id="txtDescripcionOtros" type="text" aria-describedby="nameHelp">--%>
                                        <textarea class="form-control" id="txtDescripcionOtros" rows="3" placeholder="Escribir descripcion" maxlength="300"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="container">
                        <div class="form-group">
                            <div class="form-row">
                                <div class="col-md-6">
                                    <button class="btn btn-dark" type="button" id="btnAceptarDefault" onclick="LineaOtros(document.getElementById('txtLineaHardware').id)">Aceptar</button>
                                </div>
                                <div class="col-md-6">
                                    <button class="btn btn-dark" type="button" id="btnCancelarDefault" onclick="LimpiarOtros()">Cancelar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="ModalClientes" tabindex="-1" role="dialog" aria-labelledby="ModalClientesLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="button">
            <div class="modal-content">
                <%--<input type="hidden" id="txtLineaCliente">--%>
                <%--<input type="text" id="txtLineaCodigo">--%>
                <div class="modal-header">
                    <h6 class="modal-title" id="ModalClientesLabel">Clientes</h6>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBuscarCliente" EventName="Click" />
                    </Triggers>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnQuitarFiltroCliente" EventName="Click" />
                    </Triggers>
                    <%--                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvClientes" EventName="PageIndexChanging" />
                    </Triggers>--%>
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="container">
                                <div class="card-body">
                                    <div class="form-group">
                                        <%--                                <div class="form-row">
                                    <div class="col-md-6">
                                        <label for="txtAncho">Buscar:</label>
                                        <input class="form-control" id="txtBuscarCodigo" type="text" aria-describedby="nameHelp">
                                    </div>
                                    <div class="col-md-6">
                                        <button class="btn btn-dark" type="button" id="btnBuscarCodigo">Buscar</button>
                                    </div>
                                </div>--%>
                                        <div class="form-row">
                                            <div class="col-md-6">
                                                <label for="txtAncho">Buscar:</label>
                                                <div class="input-group">
                                                    <input class="form-inline" runat="server" id="txtBuscarCliente" type="text" aria-describedby="nameHelp" onkeypress="return event.keyCode != 13;"><br />
                                                    <%--<button class="btn btn-dark" type="button" id="btnBuscarCodigos" onclick="EjecutarBusqueda()">Buscar</button>--%>
                                                    <asp:LinkButton ID="btnBuscarCliente" CssClass="btn btn-dark" runat="server" OnClick="btnBuscarCliente_Click" Text="Buscar" />
                                                    <asp:LinkButton ID="btnQuitarFiltroCliente" CssClass="btn btn-dark" runat="server" OnClick="btnQuitarFiltroCliente_Click" Text="Quitar Filtro" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:GridView ID="gvClientes" Font-Size="XX-Small" CssClass="table table-bordered" runat="server" Width="100%" DataSourceID="odsClientes" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <button class="btn btn-dark" type="button" id="btnseleccionarCliente" onclick="LineaCliente(this)">Seleccionar</button>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Razon_social" HeaderText="Razon Social" SortExpression="Razon_social" />
                                    <asp:BoundField DataField="Rut" HeaderText="Rut" SortExpression="Rut" />
                                    <asp:BoundField DataField="Contacto" HeaderText="Contacto" SortExpression="Contacto" />
                                    <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
                                    <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <div class="container">
                        <div class="form-group">
                            <div class="form-row">
                                <%--                                <div class="col-md-6">
                                    <button class="btn btn-dark" type="button" id="btnAceptarCodigo">Aceptar</button>
                                </div>--%>
                                <div class="col-md-6">
                                    <button class="btn btn-dark" id="btnCancelarClientes" type="button" data-dismiss="modal">Cancelar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>