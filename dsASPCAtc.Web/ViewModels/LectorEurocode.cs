using EntidadesAtc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dsASPCAtc.Web.ViewModels
{
    public class LectorEurocode
    {
        


        public string _eurocode;
        private List<Char> _eurosplit;
        private EntidadEurocodes _entidadEurocodes;
        private IDictionary<string, string> _tiposVidrio = new Dictionary<string, string>();
        private IDictionary<string, string> _glass = new Dictionary<string, string>();
        private IDictionary<string, string> _lunetas = new Dictionary<string, string>();
        private IDictionary<string, string> _lunetascaracter = new Dictionary<string, string>();
        private IDictionary<string, string> _toptins = new Dictionary<string, string>();
        private IDictionary<string, string> _parabrisascaracter = new Dictionary<string, string>();
        private IDictionary<string, string> _carroceriavehiculo = new Dictionary<string, string>();
        private IDictionary<string, string> _posicionvidrio = new Dictionary<string, string>();
        private IDictionary<string, string> _lateralestechoscaracter = new Dictionary<string, string>();
        private IDictionary<string, string> _tipoaccesoriosk = new Dictionary<string, string>();
        private IDictionary<string, string> _tipoaccesoriox = new Dictionary<string, string>();
        private IDictionary<string, string> _caracteristicasaccesoriossk = new Dictionary<string, string>();
        private IDictionary<string, string> _caracteristicasaccesoriosx = new Dictionary<string, string>();
        private void RellenarDiccionarioTiposVidrio()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.TiposVidrio)
            {
                _tiposVidrio.Add(pp.euro, pp.desc);
            }
            //_tiposVidrio.Add("A", "Parabrisas");
            //_tiposVidrio.Add("B", "Luneta");
            //_tiposVidrio.Add("C", "Parabrisas alternativo");
            //_tiposVidrio.Add("D", "Parabrisas con accesorios en paquete");
            //_tiposVidrio.Add("E", "Luneta con accesorios en pack");
            //_tiposVidrio.Add("F", "Lateral Plano");
            //_tiposVidrio.Add("G", "Techo de Cristal");
            //_tiposVidrio.Add("H", "Lateral plano con accesorios en pack");
            //_tiposVidrio.Add("L", "Laterales Izquierdo");
            //_tiposVidrio.Add("M", "Lateral izquiero con accesorios en pack");
            //_tiposVidrio.Add("R", "Lateral Derecho");
            //_tiposVidrio.Add("S", "Accesorios para ambos lados");
            //_tiposVidrio.Add("T", "Lateral derecho con accesorios en pack");
        }
        private void RellenarDiccionarioGlasses()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.Color)
            {
                _glass.Add(pp.euro, pp.desc);
            }
            //_glass.Add("AB", "Antivandálico incoloro (5 capas)");
            //_glass.Add("AC", "Antivandálico (2 capas con multiples capas de PVB)");
            //_glass.Add("AF", "Antibalas (2 capas de vidrio + 1 capa de policarbonato)");
            //_glass.Add("AG", "Antivandálico verde (5 capas)");
            //_glass.Add("AP", "Antivandálico privado(5 capas)");
            //_glass.Add("AS", "Vidrio de seguridad incoloro(3-capas)");
            //_glass.Add("BA", "Azul + Acústico");
            //_glass.Add("BB", "Azul - PVB absorbente de calor");
            //_glass.Add("BD", "Azul Oscuro");
            //_glass.Add("BL", "Azul");
            //_glass.Add("BP", "Azul - Vidrio privado");
            //_glass.Add("BS", "Azul - Control Solar");
            //_glass.Add("BZ", "Bronce");
            //_glass.Add("CA", "Incoloro + Acústico");
            //_glass.Add("CB", "Incoloro  - PVB Absorbente de Carlos");
            //_glass.Add("CC", "Reflectante");
            //_glass.Add("CD", "Reflectante + Acústico");
            //_glass.Add("CF", "Vidrio esmerilado o vidrio con tinte proporcionado a través de una lámina adhesiva");
            //_glass.Add("CH", "Reflectante con alto efecto de reflexión de Calor");
            //_glass.Add("CK", "Reflectante con alto efecto de reflexión de Calor + Acústico");
            //_glass.Add("CL", "Incoloro");
            //_glass.Add("GA", "Verde + Acústico");
            //_glass.Add("GB", "Verde  - PVB Absorbente de Calor");
            //_glass.Add("GC", "Verde - PVD Absorbente de calor + Acústico");
            //_glass.Add("GD", "Verde oscuro");
            //_glass.Add("GN", "Verde");
            //_glass.Add("GP", "Verde - Vidrio privado");
            //_glass.Add("GQ", "Verde - Vidrio privado acústico");
            //_glass.Add("GR", "Verde Control solar + Acústico");
            //_glass.Add("GS", "Verde control solar");
            //_glass.Add("GY", "Gris");
            //_glass.Add("LG", "Verde Japones (Claro)");
            //_glass.Add("YA", "Gris + Acústico");
            //_glass.Add("YC", "Gris reflectante");
            //_glass.Add("YD", "Gris oscuro");
            //_glass.Add("YP", "Gris - Vidrio Privado");
            //_glass.Add("YQ", "Griis - Vidrio Privado + Acústico");
            //_glass.Add("YR", "Gris - Vidrio privado reflectante");
            //_glass.Add("YS", "Gris - Control Solar");
            //_glass.Add("ZP", "Bronce - Vidrio Privado");
        }
        private void RellenarDiccionarioLunetas()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.Luneta)
            {
                _lunetas.Add(pp.euro, pp.desc);
            }
            //_lunetas.Add("H", "Hatchback/Crossover");
            //_lunetas.Add("S", "Saloon/sedan");
            //_lunetas.Add("C", "Coupe");
            //_lunetas.Add("E", "Estate/break/Ranchera");
            //_lunetas.Add("M", "MPV (todos los MPV fueron codificados como furgonetas hasta el final de 2005)");
            //_lunetas.Add("R", "Ranger/SUV");
            //_lunetas.Add("T", "Tourer sport/cabrio");
            //_lunetas.Add("L", "Camión/Truck");
            //_lunetas.Add("V", "Furgoneta/Van");
            //_lunetas.Add("P", "Pick-up");
        }
        private void RellenarDiccionarioCaracteristicasLunetas()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.CaracLuneta)
            {
                _lunetascaracter.Add(pp.euro, pp.desc);
            }
            //_lunetascaracter.Add("A", "Antena");
            //_lunetascaracter.Add("B", "'Brakelight - Refers to the following specificities identifying a brakelight : Specific silkscreenm, Specific Wire network, Specific fitting(s), Specific Bracket(s), Complete brakelight'");
            //_lunetascaracter.Add("C", "Parte Central");
            //_lunetascaracter.Add("D", "Doble acristalamiento");
            //_lunetascaracter.Add("E", "");
            //_lunetascaracter.Add("F", "Plano");
            //_lunetascaracter.Add("G", "GPS");
            //_lunetascaracter.Add("H", "");
            //_lunetascaracter.Add("I", "Hardware en el vidrio que no se usa en el proceso de ajuste del vidrio o no tiene un impacto directo en el funcionamiento de otras características");
            //_lunetascaracter.Add("J", "Antena de TV");
            //_lunetascaracter.Add("K", "Laminado");
            //_lunetascaracter.Add("L", "Izquierdo");
            //_lunetascaracter.Add("M", "Sistema de mensajes de Emergencia");
            //_lunetascaracter.Add("N", "Vidrio hidrofugo (Repelente al Agua)");
            //_lunetascaracter.Add("O", "Abierto");
            //_lunetascaracter.Add("P", "Conectores de Tlfn y móvil");
            //_lunetascaracter.Add("Q", "Antena de apertura sin llave");
            //_lunetascaracter.Add("R", "Derecho");
            //_lunetascaracter.Add("S", "Marco con vidrio deslizante y fijo");
            //_lunetascaracter.Add("T", "Banda superior");
            //_lunetascaracter.Add("U", "Frio");
            //_lunetascaracter.Add("V", "VIN");
            //_lunetascaracter.Add("W", "Premontado, hardware usado para la instalación del vidrio");
            //_lunetascaracter.Add("X", "Alarma");
            //_lunetascaracter.Add("Y", "Parte específica para vehículos con RHD (volante a la derecha)");
            //_lunetascaracter.Add("Z", "'Encapsulación a través de: Inyección, Extrusión de robot PU, Extrusión '");
            //_lunetascaracter.Add("1A", "Cambio en la serigrafía");
            //_lunetascaracter.Add("6A", "Cambio en la curvatura");
            //_lunetascaracter.Add("1B", "Apariencia o cambio en la serigrafía");
            //_lunetascaracter.Add("6B", "Cambio en la serigrafía, el hardware y la antena");
            //_lunetascaracter.Add("1C", "Cambio en las medidas y la serigrafía");
            //_lunetascaracter.Add("6C", "Cambio en el espesor del vidrio y la serigrafía");
            //_lunetascaracter.Add("1D", "Cambio en la encapsulación");
            //_lunetascaracter.Add("6D", "Unidad completa, incluyendo luz trasera y techo  duro");
            //_lunetascaracter.Add("1E", "Cambio en el térmico");
            //_lunetascaracter.Add("6E", "Unidad complta, incluyengo luz trasera y techo blando");
            //_lunetascaracter.Add("1F", "Cambio en la antena");
            //_lunetascaracter.Add("6F", "Cambio en la antena  y le hardaware");
            //_lunetascaracter.Add("1G", "Cambio en el Vin");
            //_lunetascaracter.Add("6G", "Cambio o cambio de apariencia en la banda superior");
            //_lunetascaracter.Add("1H", "Cambio o cambio de apariencia del taladro");
            //_lunetascaracter.Add("6H", "Cambio de antena y taladro");
            //_lunetascaracter.Add("1I", "Cambio de antena y serigrafía");
            //_lunetascaracter.Add("6I", "Cambio en la antena y la apariencia o cambio en la banda");
            //_lunetascaracter.Add("1J", "Cambio de mediddas");
            //_lunetascaracter.Add("6J", "Cambio de medidas y serigrafía");
            //_lunetascaracter.Add("1K", "Cambio en el hardware");
            //_lunetascaracter.Add("6K", "Cambio en el hardware y las medidas");
            //_lunetascaracter.Add("1L", "Cambio en las medidas, taladros y serigrafía");
            //_lunetascaracter.Add("6L", "");
            //_lunetascaracter.Add("1M", "Logotipo que indentifica las características de seguridad y protección");
            //_lunetascaracter.Add("6M", "Logotipo que indentifica las características de seguridad y proteccion y cambio de medidas");
            //_lunetascaracter.Add("1N", "Cambio en la serigrafía y el logotipo de características de protección y seguridad");
            //_lunetascaracter.Add("6N", "Change to tint of softtop or hardtop");
            //_lunetascaracter.Add("1O", "");
            //_lunetascaracter.Add("60", "");
            //_lunetascaracter.Add("1P", "Cambio en los taladros y cambio en el térmico");
            //_lunetascaracter.Add("6P", "");
            //_lunetascaracter.Add("1Q", "Cambio en los taladros y en la serígrafía");
            //_lunetascaracter.Add("6Q", "Cambio en los taladros, cambio en la serigrafía y cambio en la encapsulación");
            //_lunetascaracter.Add("1R", "Cambios en las medidas y taladro");
            //_lunetascaracter.Add("6R", "Cambio en las medidas y antena");
            //_lunetascaracter.Add("1S", "Cambio en las medidas y térmico");
            //_lunetascaracter.Add("6S", "");
            //_lunetascaracter.Add("1T", "Lototipo de medidas de seguridad y protección y cambio en el taladro");
            //_lunetascaracter.Add("6T", "");
            //_lunetascaracter.Add("1U", "Cambio en el térmico y la serigrafía");
            //_lunetascaracter.Add("6U", "");
            //_lunetascaracter.Add("1V", "Cambio la serigrafía y la encapsulación");
            //_lunetascaracter.Add("6V", "");
            //_lunetascaracter.Add("1W", "Cambio en el térmico y la encapsulación");
            //_lunetascaracter.Add("6W", "");
            //_lunetascaracter.Add("1X", "Cambio en la luz de freno");
            //_lunetascaracter.Add("6X", "");
            //_lunetascaracter.Add("1Y", "Cambio en la antela y el térmico");
            //_lunetascaracter.Add("6Y", "");
            //_lunetascaracter.Add("1Z", "Producto que requiere más de 15 dígitos");
            //_lunetascaracter.Add("6Z", "");
        }
        private void RellenarDiccionarioTopTins()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.TopTins)
            {
                _toptins.Add(pp.euro, pp.desc);
            }
            //_toptins.Add("BL", "Banza Azul");
            //_toptins.Add("BZ", "Banda Bronce");
            //_toptins.Add("GN", "Banda Verde");
            //_toptins.Add("GY", "Bande Gris");
            //_toptins.Add("LG", "Banda verde claro");
            //_toptins.Add("YD", "Banda Verde oscuro");
        }
        private void RellenarDiccionarioCaracteristicasParabrisas()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.CaracParabrisas)
            {
                _parabrisascaracter.Add(pp.euro, pp.desc);
            }
            //_parabrisascaracter.Add("A", "Antena");
            //_parabrisascaracter.Add("B", "RHD (Conduccíon derecha) parabrisas");
            //_parabrisascaracter.Add("C", "'Soporte y / o accesorios de la cámara: -Vision nocturna - LDW: advertencia de salida de carril - IHC: control inteligente de los faros - LC: control de luz detectando los coches en sentido contrario - TL: límite de velocidad - Funcionalidad ACC - Sistema de frenado de emergencia de la ciudad - Reconocimiento de señales de tráfico'");
            //_parabrisascaracter.Add("D", "");
            //_parabrisascaracter.Add("E", "'Sistema de mensajería de emergencia del vehículo - VICS: Sistema de información y comunicación del vehículo(tecnología utilizada en Japón)'");
            //_parabrisascaracter.Add("F", "");
            //_parabrisascaracter.Add("G", "GPS");
            //_parabrisascaracter.Add("H", "Térmico");
            //_parabrisascaracter.Add("I", "Hardware en el vidrio que no se usa en el proceso de ajuste del vidrio o no tiene un impacto directo en el funcionamiento de otras funciones");
            //_parabrisascaracter.Add("J", "Antena de TV");
            //_parabrisascaracter.Add("K", "Térmico originada a través del recubrimiento en el vidriog originated through coating on the glass");
            //_parabrisascaracter.Add("L", "Parabrisas lado izquierdo");
            //_parabrisascaracter.Add("M", "Sensor (luz y/o lluvia) o accesorios");
            //_parabrisascaracter.Add("N", "Vidrio repelente de agua (hidrofugo)");
            //_parabrisascaracter.Add("O", "Antivaho");
            //_parabrisascaracter.Add("P", "'Modificación de serigrafía para Detector de sensor, De - Vapor(desempañador) LDW, IHC, LC, TL, Sistema de frenado de emergencia de la ciudad, visión nocturna, colisión de la ciudad sistema, etc...'");
            //_parabrisascaracter.Add("Q", "");
            //_parabrisascaracter.Add("R", "Parabrisas lado derecho");
            //_parabrisascaracter.Add("S", "");
            //_parabrisascaracter.Add("T", "");
            //_parabrisascaracter.Add("U", "H.U.D (Pantalla Integrada)");
            //_parabrisascaracter.Add("V", "VIN");
            //_parabrisascaracter.Add("W", "Hardware necesario para el montaje del vidrio");
            //_parabrisascaracter.Add("X", "");
            //_parabrisascaracter.Add("Y", "");
            //_parabrisascaracter.Add("Z", "'Encapsulación a través de: Inyección Extrusión de robot PU Extrusión '");

            //_parabrisascaracter.Add("1A", "Cambios en el espesor");
            //_parabrisascaracter.Add("6A", "Corte en el PVB en vidrio reflectante");
            //_parabrisascaracter.Add("1B", "Apariencia o cambio en la serigrafía");
            //_parabrisascaracter.Add("6B", "Cambia la serigrafía y el hardware");
            //_parabrisascaracter.Add("1C", "Aspecto o cambio el soporte de espejo");
            //_parabrisascaracter.Add("6C", "Cambia el soporte de espejo y el logotipo para identificar las funciones de seguridad y protección");
            //_parabrisascaracter.Add("1D", "Cambia la encapsulación");
            //_parabrisascaracter.Add("6D", "Cambia VIN y cambia el soporte de espejo");
            //_parabrisascaracter.Add("1E", "Cambio en el térmico");

            //_parabrisascaracter.Add("6E", "Cambio en el térmico y el hardware");
            //_parabrisascaracter.Add("1F", "Cambia la antena");
            //_parabrisascaracter.Add("6F", "Cambia la antena y el soporte de espejo");
            //_parabrisascaracter.Add("1G", "Cambia el VIN");
            //_parabrisascaracter.Add("6G", "Mismo tono de color pero mas oscuro");
            //_parabrisascaracter.Add("1H", "Cambio la posición del soporte del espejo");
            //_parabrisascaracter.Add("6H", "Cambia la serigrafía, el sensor y el térmico");
            //_parabrisascaracter.Add("1I", "Cambia la serigrafía, el soporte y el hardware");
            //_parabrisascaracter.Add("6I", "Cambia la serigrafía, el soporte o la posición y el logotipo de caracteristicas");
            //_parabrisascaracter.Add("1J", "Cambia la encapsulacion y la posicion del espejo");
            //_parabrisascaracter.Add("6J", "Cambia la encalsulacion y la serigrafía");
            //_parabrisascaracter.Add("1K", "Cambio en el hardware");
            //_parabrisascaracter.Add("6K", "Cambia el soporte de espejo, la serigrafía y la encapsulación");
            //_parabrisascaracter.Add("1L", "Change to camera bracket and to features");
            //_parabrisascaracter.Add("6L", "Cambia el soporte, la serigrafía y el espesor del vidrio");
            //_parabrisascaracter.Add("1M", "Incluye logotipo que identifica las características de seguridad y protección");
            //_parabrisascaracter.Add("6M", "Cambia el hardware y el logotipo de características de seguridad y protección");
            //_parabrisascaracter.Add("1N", "Cambia la serigrafía y el espesor del vidrio");
            //_parabrisascaracter.Add("6N", "Cambia la serigrafía y el grosor del vidrio junto al logotipo de funciones de seguridad y protección");
            //_parabrisascaracter.Add("1O", "Cambio en la ventana VIN y antena");
            //_parabrisascaracter.Add("6O", "Cambia la serigrafía y la antena");
            //_parabrisascaracter.Add("1P", "Cambia el soporte y serigrafía");
            //_parabrisascaracter.Add("6P", "Cambia el soporte, la serigrafía y el sensor");
            //_parabrisascaracter.Add("1Q", "Cambia el VIN y el hardware");
            //_parabrisascaracter.Add("6Q", "Cambia la camara´y las funcionalidades, la serigrafía y el soporte.");
            //_parabrisascaracter.Add("1R", "Logo que indentifica las funciones de seguridad y protección y cambia la serigrafía");
            //_parabrisascaracter.Add("6R", "Logo que indentifica las funciones de seguridad y protección y cambia la serigrafía y el VIN");
            //_parabrisascaracter.Add("1S", "Cambia la serigrafía y el térmico");
            //_parabrisascaracter.Add("6S", "Cambia la camara y su funcinalidad, la serigrafía, el térmico y el soporte.");
            //_parabrisascaracter.Add("1T", "Cambio en el sensor");
            //_parabrisascaracter.Add("6T", "Cambio en el sensor y la serigrafía");
            //_parabrisascaracter.Add("1U", "Cambia la serigrafía, el soporte y el térmico");
            //_parabrisascaracter.Add("6U", "Cambia el térmico, sensor, serigrafía y hardware");
            //_parabrisascaracter.Add("1V", "Cambia la serigrafía y la posición del soporte");
            //_parabrisascaracter.Add("6V", "Cambia la camara y su funcinalidad, tambien cambia el grosor del vidrio");
            //_parabrisascaracter.Add("1W", "Cambia el sensor, la serigrafía y el hardware");
            //_parabrisascaracter.Add("6W", "Cambia el soporte y el hardware");
            //_parabrisascaracter.Add("1X", "Cambia el soporte, el soporte de la cámara y las funcionalidades");
            //_parabrisascaracter.Add("6X", "Logotipo que indenfitica las caracteristias de seguridad y protección, además cambio en el sensor y la serigrafía");

            //_parabrisascaracter.Add("1Y", "Cambio mínimo en las dimensiones y cambios en el hardware");
            //_parabrisascaracter.Add("6Y", "Producto codificado con las caterísticas D/F/S/T/Y antes del 21/09/05");
            //_parabrisascaracter.Add("1Z", "Producto que requiere mas de 15 dígitos para su codificación");
            //_parabrisascaracter.Add("6Z", "Cambios mínimos en las dimensiones");
            //_parabrisascaracter.Add("10", "Apariencia o cambio en la serigrafía y cambio la cámara y características");
            //_parabrisascaracter.Add("15", "Cambia la encapsulación, el sensor, y la serigrafía");
            //_parabrisascaracter.Add("20", "");
            //_parabrisascaracter.Add("25", "");
            //_parabrisascaracter.Add("30", "");
            //_parabrisascaracter.Add("35", "Cambia la posición del sensor y la serigrafía");
            //_parabrisascaracter.Add("40", "Cambia la posicion del sensor, la serigrafía y el térmico.");
            //_parabrisascaracter.Add("45", "Cambia la posición del sensor, la serigrafía y el hardware");
            //_parabrisascaracter.Add("50", "Cambia la serigrafía, en sensor, el térmico y el VIN");
            //_parabrisascaracter.Add("55", "Cambia la posición del sensor, la serigrafía y la encapulación");
            //_parabrisascaracter.Add("60", "Cambia la serigrafía, la posicíon del soporte y la encapsulación");
            //_parabrisascaracter.Add("65", "Cambia el sensor, la posicíon del sensor, el soporte y la posición del soporte, además de la serigrafía");
            //_parabrisascaracter.Add("70", "");
            //_parabrisascaracter.Add("75", "Cambia el soporte, la posición del mismo y la serigrafía");
            //_parabrisascaracter.Add("80", "");
            //_parabrisascaracter.Add("85", "");
            //_parabrisascaracter.Add("90", "");
            //_parabrisascaracter.Add("95", "Modificación específica detallada en la descripción de la pieza");
        }
        private void RellenarDiccionarioCarroceriasVehiculos()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.CarroceriasVehiculos)
            {
                _carroceriavehiculo.Add(pp.euro, pp.desc);
            }
            //_carroceriavehiculo.Add("H3", "Hatchback/Crossover 3 puertas");
            //_carroceriavehiculo.Add("H4", "Hatchback/Crossover 4 puertas");
            //_carroceriavehiculo.Add("H5", "Hatchback/Crossover 5 puertas");
            //_carroceriavehiculo.Add("S2", "Saloon/sedan 2 puertas");
            //_carroceriavehiculo.Add("S4", "Saloon/sedan 4 puertas");
            //_carroceriavehiculo.Add("S6", "Limusina 6 puertas");
            //_carroceriavehiculo.Add("C2", "Coupe 2 puertas");
            //_carroceriavehiculo.Add("C4", "Coupe 4 puertas");
            //_carroceriavehiculo.Add("C5", "Coupe 5 puertas");
            //_carroceriavehiculo.Add("E3", "Estate/break/Ranchera 3 puertas");
            //_carroceriavehiculo.Add("E5", "Estate/break/Ranchera 5 puertas");
            //_carroceriavehiculo.Add("E6", "Estate/break/Ranchera 6 puertas");
            //_carroceriavehiculo.Add("M3", "MPV 3 puertas");
            //_carroceriavehiculo.Add("M4", "MPV 4 puertas");
            //_carroceriavehiculo.Add("M5", "MPV 5 puertas");
            //_carroceriavehiculo.Add("R3", "Ranger/SUV 3 puertas");
            //_carroceriavehiculo.Add("R5", "Ranger/SUV 5 puertas");
            //_carroceriavehiculo.Add("T2", "Tourer Sport/cabrio 2 puertas");
            //_carroceriavehiculo.Add("L2", "Lorry/truck 2 puertas");
            //_carroceriavehiculo.Add("L4", "Lorry/truck 4 puertas");
            //_carroceriavehiculo.Add("V2", "Furgoneta/Van 2 puertas");
            //_carroceriavehiculo.Add("V3", "Furgoneta/Van 3 puertas");
            //_carroceriavehiculo.Add("V4", "Furgoneta/Van 4 puertas");
            //_carroceriavehiculo.Add("V5", "Furgoneta/Van 5 puertas");
            //_carroceriavehiculo.Add("P2", "Pick-up 2 puertas");
            //_carroceriavehiculo.Add("P4", "Pick-up 4 puertas");
        }
        private void RellenarDiccionarioPosicionesVidrios()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.PosicionesVidrios)
            {
                _posicionvidrio.Add(pp.euro, pp.desc);
            }
            //_posicionvidrio.Add("FD", "Descente delantero");
            //_posicionvidrio.Add("FQ", "Custodia delantera");
            //_posicionvidrio.Add("FV", "Fijo delantero");
            //_posicionvidrio.Add("MD", "Descendente central/Puerta central");
            //_posicionvidrio.Add("MQ", "Fijo puerta central/Zona central");
            //_posicionvidrio.Add("PG", "Partition glass");
            //_posicionvidrio.Add("RD", "Descendente trasero");
            //_posicionvidrio.Add("RQ", "Custodia trasera");
            //_posicionvidrio.Add("RV", "Fijo trasero");
            //_posicionvidrio.Add("GP", "Panel de vidrio");
            //_posicionvidrio.Add("GR", "Techo de cristal");
            //_posicionvidrio.Add("LA", "Lamina");
            //_posicionvidrio.Add("LR", "Techo solar Grande");
            //_posicionvidrio.Add("PR", "Techo panorámico");
            //_posicionvidrio.Add("SR", "Techo solar OE");
            //_posicionvidrio.Add("SS", "Techo solar con panel solar");
        }
        private void RellenarDiccionarioCaracteristicasLateralesTechos()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.CaracLateralesTechos)
            {
                _lateralestechoscaracter.Add(pp.euro, pp.desc);
            }
            //_lateralestechoscaracter.Add("A", "Antena");
            //_lateralestechoscaracter.Add("B", "Parte de abajo");
            //_lateralestechoscaracter.Add("C", "");
            //_lateralestechoscaracter.Add("D", "Doble acristalamiento");
            //_lateralestechoscaracter.Add("E", "Operado electricamente");
            //_lateralestechoscaracter.Add("F", "Marco con vidrio deslizando y fijo");
            //_lateralestechoscaracter.Add("G", "GPS");
            //_lateralestechoscaracter.Add("H", "Térmico");
            //_lateralestechoscaracter.Add("I", "Hardware en el vidrio que no se usa en el proceso de ajuste del vidrio o no tiene un impacto directo en el funcionamiento de otras funciones");
            //_lateralestechoscaracter.Add("J", "Antena de TV");
            //_lateralestechoscaracter.Add("K", "Laminado");
            //_lateralestechoscaracter.Add("L", "");
            //_lateralestechoscaracter.Add("M", "");
            //_lateralestechoscaracter.Add("N", "Cristal Hidrofugo (Repelente de Agua)");
            //_lateralestechoscaracter.Add("O", "Abierto");
            //_lateralestechoscaracter.Add("P", "Antena de calentamiento");
            //_lateralestechoscaracter.Add("Q", "Antena de apertura sin llave");
            //_lateralestechoscaracter.Add("R", "");
            //_lateralestechoscaracter.Add("S", "Corredizo");
            //_lateralestechoscaracter.Add("T", "Mitad para usar en un marco");
            //_lateralestechoscaracter.Add("U", "Superior");
            //_lateralestechoscaracter.Add("V", "VIN");
            //_lateralestechoscaracter.Add("W", "Premontado para usar en el montaje del vidrio");
            //_lateralestechoscaracter.Add("X", "Alarma");
            //_lateralestechoscaracter.Add("Y", "Específico para vehículos con RHD (manejo a la derecha)");
            //_lateralestechoscaracter.Add("Z", "'Encapsulación a través de:Inyección Extrusión de robot PU Extrusión '");
            //_lateralestechoscaracter.Add("1A", "Cambio en el espesor del vidrio");
            //_lateralestechoscaracter.Add("6A", "2º vidrio corredizo en el vidrio de la puerta");
            //_lateralestechoscaracter.Add("1B", "Cambio en la serigrafía");
            //_lateralestechoscaracter.Add("6B", "Cambio o cambio de apariencia en la serigrafía y el espesor del vidrio");
            //_lateralestechoscaracter.Add("1C", "Cambio en las medidas y serigrañfia");
            //_lateralestechoscaracter.Add("6C", "Cambio en la encapsulacion y la serigrafía");
            //_lateralestechoscaracter.Add("1D", "Cambio en el tipo de encapsulado ( P. ejemplo de negro mate a cromado)");
            //_lateralestechoscaracter.Add("6D", "Cambio el tipo de encapsulado y el espesor");
            //_lateralestechoscaracter.Add("1E", "Cambio en el térmico");
            //_lateralestechoscaracter.Add("6E", "Cambio en las medidas y le encapsulación");
            //_lateralestechoscaracter.Add("1F", "Cambio en la antena");
            //_lateralestechoscaracter.Add("6F", "Cambio en la antena y la encapsulación");
            //_lateralestechoscaracter.Add("1G", "Cambio en la encapsulacion ");
            //_lateralestechoscaracter.Add("6G", "Cambio en el PVB intercalado");
            //_lateralestechoscaracter.Add("1H", "Cambio de taladros");
            //_lateralestechoscaracter.Add("6H", "Cambio de medidas y PVB intercalado");
            //_lateralestechoscaracter.Add("1I", "Cambio de medidas y tipo de encapsulado");
            //_lateralestechoscaracter.Add("6I", "Cambio del tipo de encapsulado y antena");
            //_lateralestechoscaracter.Add("1J", "Cambio de medidas");
            //_lateralestechoscaracter.Add("6J", "Cambio en las meddias y el logotipo de indentificación de seguridad y protección");
            //_lateralestechoscaracter.Add("1K", "Cambio en el hardare");
            //_lateralestechoscaracter.Add("6K", "Cambio en las medidas, taladros y hardware");
            //_lateralestechoscaracter.Add("1L", "Cambio en el encapsulado y logotipo que identifica las funciones de seguridad y protección");
            //_lateralestechoscaracter.Add("6L", "Cambia el hardware y la serigrafía");
            //_lateralestechoscaracter.Add("1M", "Logotipo que identifica las funciones de seguridad y protección");
            //_lateralestechoscaracter.Add("6M", "Cambio medidas, antena y encapsulado");
            //_lateralestechoscaracter.Add("1N", "Cambio de la serigrafía y el logo que identifica las funciones de seguridad y protección");
            //_lateralestechoscaracter.Add("6N", "Cambio en la antena y el logo que identifica las funciones de seguridad y protección");
            //_lateralestechoscaracter.Add("1O", "Cambio del tinte de vidrio mediante la aplicación de una lámina");
            //_lateralestechoscaracter.Add("60", "Cambia la encapsulación, el tono de encapsulación y antena");
            //_lateralestechoscaracter.Add("1P", "Cambian las medidas y el espesor del vidrio");
            //_lateralestechoscaracter.Add("6P", "Cambia la antena, el encapsulado y el logotipo que indentifica las funciones de seguridad y proteccion");
            //_lateralestechoscaracter.Add("1Q", "Cambia la serigrafía y el taladro");
            //_lateralestechoscaracter.Add("6Q", "Cambia el espesor y taladro");
            //_lateralestechoscaracter.Add("1R", "Cambia las medidas y el taladro");
            //_lateralestechoscaracter.Add("6R", "Cambias las medidas y el hardware (premontado)");
            //_lateralestechoscaracter.Add("1S", "Cambias las meddias y la antena");
            //_lateralestechoscaracter.Add("6S", "Camban las medidas, la antena y el tono de encapsulación");
            //_lateralestechoscaracter.Add("1T", "Cambia el hardware y el premontado");
            //_lateralestechoscaracter.Add("6T", "");
            //_lateralestechoscaracter.Add("1U", "");
            //_lateralestechoscaracter.Add("6U", "");
            //_lateralestechoscaracter.Add("1V", "");
            //_lateralestechoscaracter.Add("6V", "");
            //_lateralestechoscaracter.Add("1W", "");
            //_lateralestechoscaracter.Add("6W", "");
            //_lateralestechoscaracter.Add("1X", "");
            //_lateralestechoscaracter.Add("6X", "");
            //_lateralestechoscaracter.Add("1Y", "");
            //_lateralestechoscaracter.Add("6Y", "");
            //_lateralestechoscaracter.Add("1Z", "Producto que requiere mas de 15 dígitos");
            //_lateralestechoscaracter.Add("6Z", "");
        }
        private void RellenarDiccionarioTipoAccesorio()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.TipoAccesorioSK)
            {
                _tipoaccesoriosk.Add(pp.euro, pp.desc);
            }
            foreach (PropiedadYValor pp in _entidadEurocodes.TipoAccesorioX)
            {
                _tipoaccesoriox.Add(pp.euro, pp.desc);
            }
            //_tipoaccesoriosk.Add("A", "Antena GPS");
            //_tipoaccesoriosk.Add("B", "Soporte de Espejo");
            //_tipoaccesoriosk.Add("C", "Clip/tape");
            //_tipoaccesoriosk.Add("D", "Aleron");
            //_tipoaccesoriosk.Add("E", "Embellecedor del pilar");
            //_tipoaccesoriosk.Add("F", "Fillerstrip/insert");
            //_tipoaccesoriosk.Add("G", "Glazing channel");
            //_tipoaccesoriosk.Add("H", "Cubierta de agujero para el limpia parabrisas");
            //_tipoaccesoriosk.Add("I", "Insercion de guia");
            //_tipoaccesoriosk.Add("J", "Junta");
            //_tipoaccesoriosk.Add("K", "Grapas + Molduras");
            //_tipoaccesoriosk.Add("L", "Sensor de Luz");
            //_tipoaccesoriosk.Add("M", "Molduras");
            //_tipoaccesoriosk.Add("N", "Sensor de Lluvia");
            //_tipoaccesoriosk.Add("P", "Membrana de la puerta / membrana de protección del parabrisas");
            //_tipoaccesoriosk.Add("Q", "Elevalunas + Sistema de Bloqueo");
            //_tipoaccesoriosk.Add("R", "Goma");
            //_tipoaccesoriosk.Add("S", "Remache");
            //_tipoaccesoriosk.Add("V", "Sensor antiempañamiento");
            //_tipoaccesoriosk.Add("W", "Panel del limpiaparabrisas");

            //_tipoaccesoriox.Add("A", "Antena");
            //_tipoaccesoriox.Add("B", "Soporte para sensor o cámara");
            //_tipoaccesoriox.Add("C", "Cámara para todo tipo de sistemas de seguridad y asistencia");
            //_tipoaccesoriox.Add("D", "");
            //_tipoaccesoriox.Add("E", "Cable / cable eléctrico, conector o arnés");
            //_tipoaccesoriox.Add("F", "Luz de freno, luz interior");
            //_tipoaccesoriox.Add("G", "Repuestos de GPS que no sean de antena");
            //_tipoaccesoriox.Add("H", "'Manija, bisagra, rieles de techo'");
            //_tipoaccesoriox.Add("I", "Spoiler");
            //_tipoaccesoriox.Add("J", "Chorro de agua");
            //_tipoaccesoriox.Add("K", "Pegatina o cinta adhesiva / cinta adhesiva");
            //_tipoaccesoriox.Add("L", "Sistema o elemento de bloqueo - eléctrico o manual");
            //_tipoaccesoriox.Add("M", "Espejo");
            //_tipoaccesoriox.Add("N", "");
            //_tipoaccesoriox.Add("P", "Almohadillas adhesivas para sensor u otras partes");
            //_tipoaccesoriox.Add("Q", "Calefacción interior, aire acondicionado, sistemas de filtrado");
            //_tipoaccesoriox.Add("R", "Espejo retrovisor");
            //_tipoaccesoriox.Add("S", "Sensor gel");
            //_tipoaccesoriox.Add("V", "Cortinas");
            //_tipoaccesoriox.Add("W", "Brazo del limpiaparabrisas, soporte del brazo del limpiaparabrisas");
            //_tipoaccesoriox.Add("X", "Adhesivo o placa con logo");
            //_tipoaccesoriox.Add("Y", "");
            //_tipoaccesoriox.Add("Z", "Marco para vidrio, hardware para ensamblaje de vidrio, barra separadora");

        }
        private void RellenarDiccionarioCaracteristicasAccesorio()
        {
            foreach (PropiedadYValor pp in _entidadEurocodes.CaracAccesorioSK)
            {
                _caracteristicasaccesoriossk.Add(pp.euro, pp.desc);
            }
            foreach (PropiedadYValor pp in _entidadEurocodes.CaracAccesorioX)
            {
                _caracteristicasaccesoriosx.Add(pp.euro, pp.desc);
            }
            //_caracteristicasaccesoriossk.Add("A", "");
            //_caracteristicasaccesoriossk.Add("B", "Parte Inferior");
            //_caracteristicasaccesoriossk.Add("C", "Cromado");
            //_caracteristicasaccesoriossk.Add("D", "Moldura de techo");
            //_caracteristicasaccesoriossk.Add("E", "Operado electricamente");
            //_caracteristicasaccesoriossk.Add("F", "Borde delantero (solo se aplica a los laterales)");
            //_caracteristicasaccesoriossk.Add("G", "Canaleta");
            //_caracteristicasaccesoriossk.Add("H", "'Borde trasero (solo se aplica a los laterales)'");
            //_caracteristicasaccesoriossk.Add("I", "Interior");
            //_caracteristicasaccesoriossk.Add("J", "Estuche del sensor con lente solamente");
            //_caracteristicasaccesoriossk.Add("K", "Esquina");
            //_caracteristicasaccesoriossk.Add("L", "Izquiero");
            //_caracteristicasaccesoriossk.Add("M", "Acabado de goma");
            //_caracteristicasaccesoriossk.Add("N", "");
            //_caracteristicasaccesoriossk.Add("O", "Exterior");
            //_caracteristicasaccesoriossk.Add("P", "Bracket");
            //_caracteristicasaccesoriossk.Add("Q", "Carcasa");
            //_caracteristicasaccesoriossk.Add("R", "Derecho");
            //_caracteristicasaccesoriossk.Add("S", "No guarda mano");
            //_caracteristicasaccesoriossk.Add("T", "Parte Superior");
            //_caracteristicasaccesoriossk.Add("U", "Perno / Tornillo");
            //_caracteristicasaccesoriossk.Add("V", "Vertical");
            //_caracteristicasaccesoriossk.Add("W", "Accesorios");
            //_caracteristicasaccesoriossk.Add("X", "");
            //_caracteristicasaccesoriossk.Add("Y", "");
            //_caracteristicasaccesoriossk.Add("Z", "");
            //_caracteristicasaccesoriossk.Add("1A", "Cambia la longuitud del Perfil");
            //_caracteristicasaccesoriossk.Add("6A", "Cambia el grosor del perfil");
            //_caracteristicasaccesoriossk.Add("1B", "Diferente tipo de clip para el mismo lado del vidio");
            //_caracteristicasaccesoriossk.Add("6B", "Diferente tipo de clip para el mismo vidrio");
            //_caracteristicasaccesoriossk.Add("1C", "Cambia el color de la moldura / clip");
            //_caracteristicasaccesoriossk.Add("6C", "Cambia el material de fabricacion");
            //_caracteristicasaccesoriossk.Add("1D", "Cambio el refuerzo interno");
            //_caracteristicasaccesoriossk.Add("6D", "Cambia el espesor y el material de fabricacion");
            //_caracteristicasaccesoriossk.Add("1E", "Diferente tipo de clip para los laterales");
            //_caracteristicasaccesoriossk.Add("6E", "Diferente tipo de clip para los laterales");
            //_caracteristicasaccesoriossk.Add("1F", "Official A.R.G.I.C. kit");
            //_caracteristicasaccesoriossk.Add("6F", "Official A.R.G.I.C. kit");
            //_caracteristicasaccesoriossk.Add("1G", "Non-official A.R.G.I.C. kit");
            //_caracteristicasaccesoriossk.Add("6G", "Non-official A.R.G.I.C. kit");
            //_caracteristicasaccesoriossk.Add("1H", "Cambia el diseño del perfil");
            //_caracteristicasaccesoriossk.Add("6H", "Producto con diferentes aplicaciones en el mismo lado del vidrio");
            //_caracteristicasaccesoriossk.Add("1I", "Cambio en el color del moldeado");
            //_caracteristicasaccesoriossk.Add("6I", "Cambio en el color del moldeado");
            //_caracteristicasaccesoriossk.Add("1J", "Diferentes tipos de grapas para el mismo vidrio");
            //_caracteristicasaccesoriossk.Add("6J", "");
            //_caracteristicasaccesoriossk.Add("1K", "Diferente tipo de perfil para diferentes tipos de carrocerías");
            //_caracteristicasaccesoriossk.Add("6K", "Específico para vehículos con RHD (conducción a la derecha)");
            //_caracteristicasaccesoriossk.Add("1L", "Logotipo que identifica características de seguridad y cambio de tamaño");
            //_caracteristicasaccesoriossk.Add("6L", "Sensor de humedad diferente para el mismo vehículo");
            //_caracteristicasaccesoriossk.Add("1M", "Diferente tipo de mecanismo de puerta para la misma carrocería");
            //_caracteristicasaccesoriossk.Add("6M", "Diferente tipo de antena para el mismo vehículo");
            //_caracteristicasaccesoriossk.Add("1N", "Diferentes tipos de mecanismo de puerta para diferentes carrocerías");
            //_caracteristicasaccesoriossk.Add("6N", "Diferente tipo de clip para el mismo vidrio");
            //_caracteristicasaccesoriossk.Add("1O", "");
            //_caracteristicasaccesoriossk.Add("60", "");
            //_caracteristicasaccesoriossk.Add("1P", "Cambia el diseño y la longitud del perfil");
            //_caracteristicasaccesoriossk.Add("6P", "");
            //_caracteristicasaccesoriossk.Add("1Q", "Kit completo o piezas con accesorios");
            //_caracteristicasaccesoriossk.Add("6Q", "");
            //_caracteristicasaccesoriossk.Add("1R", "Cambio en el color del moldeado");
            //_caracteristicasaccesoriossk.Add("6R", "");
            //_caracteristicasaccesoriossk.Add("1S", "");
            //_caracteristicasaccesoriossk.Add("6S", "");
            //_caracteristicasaccesoriossk.Add("1T", "");
            //_caracteristicasaccesoriossk.Add("6T", "");
            //_caracteristicasaccesoriossk.Add("1U", "");
            //_caracteristicasaccesoriossk.Add("6U", "");
            //_caracteristicasaccesoriossk.Add("1V", "");
            //_caracteristicasaccesoriossk.Add("6V", "");
            //_caracteristicasaccesoriossk.Add("1W", "");
            //_caracteristicasaccesoriossk.Add("6W", "");
            //_caracteristicasaccesoriossk.Add("1X", "");
            //_caracteristicasaccesoriossk.Add("6X", "");
            //_caracteristicasaccesoriossk.Add("1Y", "");
            //_caracteristicasaccesoriossk.Add("6Y", "Diferentes tipos de accesorios especiales que se definirán en la descripción");
            //_caracteristicasaccesoriossk.Add("1Z", "Producto que requiere más de 15 dígitos");
            //_caracteristicasaccesoriossk.Add("6Z", "Diferentes tipos de accesorios especiales que se definirán en la descripción");
            //_caracteristicasaccesoriosx.Add("A", "Autoadhesivo");
            //_caracteristicasaccesoriosx.Add("B", "Parte inferior");
            //_caracteristicasaccesoriosx.Add("C", "Carcasa");
            //_caracteristicasaccesoriosx.Add("D", "");
            //_caracteristicasaccesoriosx.Add("E", "Motor");
            //_caracteristicasaccesoriosx.Add("F", "Accesorios");
            //_caracteristicasaccesoriosx.Add("G", "");
            //_caracteristicasaccesoriosx.Add("H", "Parte trasera");
            //_caracteristicasaccesoriosx.Add("I", "Interior");
            //_caracteristicasaccesoriosx.Add("J", "Articulación, conector, retención");
            //_caracteristicasaccesoriosx.Add("K", "Electrocromático");
            //_caracteristicasaccesoriosx.Add("L", "Izquierda");
            //_caracteristicasaccesoriosx.Add("M", "Mecanismo, amplificador");
            //_caracteristicasaccesoriosx.Add("N", "");
            //_caracteristicasaccesoriosx.Add("O", "");
            //_caracteristicasaccesoriosx.Add("P", "Plástico");
            //_caracteristicasaccesoriosx.Add("Q", "");
            //_caracteristicasaccesoriosx.Add("R", "Derecho");
            //_caracteristicasaccesoriosx.Add("S", "Basado en slilicio");
            //_caracteristicasaccesoriosx.Add("T", "Parte Superior");
            //_caracteristicasaccesoriosx.Add("U", "");
            //_caracteristicasaccesoriosx.Add("V", "Parte delantera");
            //_caracteristicasaccesoriosx.Add("W", "Alambre, plomo");
            //_caracteristicasaccesoriosx.Add("X", "");
            //_caracteristicasaccesoriosx.Add("Y", "Husillo para el limpiaparabrisas");
            //_caracteristicasaccesoriosx.Add("Z", "");
            //_caracteristicasaccesoriosx.Add("1A", "");
            //_caracteristicasaccesoriosx.Add("6A", "");
            //_caracteristicasaccesoriosx.Add("1B", "");
            //_caracteristicasaccesoriosx.Add("6B", "");
            //_caracteristicasaccesoriosx.Add("1C", "Tono de accesorio diferente");
            //_caracteristicasaccesoriosx.Add("6C", "");
            //_caracteristicasaccesoriosx.Add("1D", "Cambio de identificación en la pegatina");
            //_caracteristicasaccesoriosx.Add("6D", "");
            //_caracteristicasaccesoriosx.Add("1E", "Kit completo o piezas con accesorios");
            //_caracteristicasaccesoriosx.Add("6E", "");
            //_caracteristicasaccesoriosx.Add("1F", "Cambio de diseño de accesorios para usos específicos");
            //_caracteristicasaccesoriosx.Add("6F", "");
            //_caracteristicasaccesoriosx.Add("1G", "Diferentes tipos de accesorios para el mismo accesorio");
            //_caracteristicasaccesoriosx.Add("6G", "");
            //_caracteristicasaccesoriosx.Add("1H", "Cambio en el diseño del accesorio");
            //_caracteristicasaccesoriosx.Add("6H", "");
            //_caracteristicasaccesoriosx.Add("1I", "");
            //_caracteristicasaccesoriosx.Add("6I", "");
            //_caracteristicasaccesoriosx.Add("1J", "");
            //_caracteristicasaccesoriosx.Add("6J", "Lámina para cambiar el tono del vidrio");
            //_caracteristicasaccesoriosx.Add("1K", "Producto que se aplica específicamente a los rieles del techo");
            //_caracteristicasaccesoriosx.Add("6K", "Producto específico para vehículos con RHD (conducción a la derecha)");
            //_caracteristicasaccesoriosx.Add("1L", "Diferente tipo de sensor para el mismo vehículo");
            //_caracteristicasaccesoriosx.Add("6L", "");
            //_caracteristicasaccesoriosx.Add("1M", "");
            //_caracteristicasaccesoriosx.Add("6M", "");
            //_caracteristicasaccesoriosx.Add("1N", "");
            //_caracteristicasaccesoriosx.Add("6N", "");
            //_caracteristicasaccesoriosx.Add("1O", "");
            //_caracteristicasaccesoriosx.Add("60", "");
            //_caracteristicasaccesoriosx.Add("1P", "");
            //_caracteristicasaccesoriosx.Add("6P", "");
            //_caracteristicasaccesoriosx.Add("1Q", "");
            //_caracteristicasaccesoriosx.Add("6Q", "");
            //_caracteristicasaccesoriosx.Add("1R", "");
            //_caracteristicasaccesoriosx.Add("6R", "");
            //_caracteristicasaccesoriosx.Add("1S", "");
            //_caracteristicasaccesoriosx.Add("6S", "");
            //_caracteristicasaccesoriosx.Add("1T", "");
            //_caracteristicasaccesoriosx.Add("6T", "");
            //_caracteristicasaccesoriosx.Add("1U", "");
            //_caracteristicasaccesoriosx.Add("6U", "");
            //_caracteristicasaccesoriosx.Add("1V", "");
            //_caracteristicasaccesoriosx.Add("6V", "");
            //_caracteristicasaccesoriosx.Add("1W", "");
            //_caracteristicasaccesoriosx.Add("6W", "");
            //_caracteristicasaccesoriosx.Add("1X", "");
            //_caracteristicasaccesoriosx.Add("6X", "");
            //_caracteristicasaccesoriosx.Add("1Y", "");
            //_caracteristicasaccesoriosx.Add("6Y", "");
            //_caracteristicasaccesoriosx.Add("1Z", "");
            //_caracteristicasaccesoriosx.Add("6Z", "");


        }

        private void RellenarDiccionarios()
        {
            RellenarDiccionarioTiposVidrio();
            RellenarDiccionarioGlasses();
            RellenarDiccionarioLunetas();
            RellenarDiccionarioCaracteristicasLunetas();
            RellenarDiccionarioTopTins();
            RellenarDiccionarioCaracteristicasParabrisas();
            RellenarDiccionarioCarroceriasVehiculos();
            RellenarDiccionarioPosicionesVidrios();
            RellenarDiccionarioCaracteristicasLateralesTechos();
            RellenarDiccionarioTipoAccesorio();
        }
        public LectorEurocode(string eurocode, EntidadEurocodes entidadEurocodes)
        {
            try
            {
                _eurocode = eurocode;
                _eurosplit = _eurocode.ToCharArray().ToList();
            }
            catch (Exception ex)
            {

            }
            _entidadEurocodes = entidadEurocodes;
            RellenarDiccionarios();
        }
        public ArticuloEurocode Leer()
        {
            var obj = new ArticuloEurocode();
            try
            {
                //var res = new List<CaracteristicaEurocode>();

                //res.Add(new CaracteristicaEurocode(_tiposVidrio[_eurocode[4].ToString()], _eurocode[4].ToString(), "TipoVidrio"));
                obj.TipoVidrio = _tiposVidrio[_eurocode[4].ToString()];
                obj.CaracterTipoVidrio = _eurocode[4].ToString();

                try
                {
                    switch (_eurocode[5].ToString())
                    {
                        case "K":
                        case "S":
                            //res = res.Concat(LeerAccesorioSK()).ToList();
                            obj = LeerAccesorioSK(obj);
                            break;
                        case "X":
                            //res = res.Concat(LeerAccesorioX()).ToList();
                            obj = LeerAccesorioX(obj);
                            break;
                        default:
                            //res = res.Concat(LeerVidrio(_eurocode[4].ToString())).ToList();
                            obj = LeerVidrio(_eurocode[4].ToString(), obj);
                            break;
                    }
                }
                catch (Exception ex)
                {

                }
            } catch (Exception ex)
            {

            }
            
            return obj;
        }
        private List<CaracteristicaEurocode> LeerVidrio(string tipoVidrio)
        {
            var res = new List<CaracteristicaEurocode>();
            var glass = _eurosplit[5].ToString() + _eurosplit[6].ToString();
            res.Add(new CaracteristicaEurocode(_glass[glass], glass, "Glass"));
            try
            {
                switch (tipoVidrio)
                {
                    case "B":
                    case "E":
                        res = res.Concat(LeerLuneta(tipoVidrio)).ToList();
                        break;
                    case "A":
                    case "C":
                    case "D":
                        res = res.Concat(LeerParabrisas(tipoVidrio)).ToList();
                        break;
                    default:
                        res = res.Concat(LeerLateralesTechos(tipoVidrio)).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                var hasdf = 1;
                var cdfja = hasdf + 1;
            }
            return res;
        }
        private ArticuloEurocode LeerVidrio(string tipoVidrio, ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            var glass = _eurosplit[5].ToString() + _eurosplit[6].ToString();
            //res.Add(new CaracteristicaEurocode(_glass[glass], glass, "Glass"));
            obj.Color = _glass[glass];
            obj.CaracterColor = glass;
            obj.PosicionCaracterColor = new int[] { 5, 6 };
            try
            {
                switch (tipoVidrio)
                {
                    case "B":
                    case "E":
                        //res = res.Concat(LeerLuneta(tipoVidrio)).ToList();
                        obj = LeerLuneta(tipoVidrio, obj);
                        break;
                    case "A":
                    case "C":
                    case "D":
                        //res = res.Concat(LeerParabrisas(tipoVidrio)).ToList();
                        obj = LeerParabrisas(tipoVidrio, obj);
                        break;
                    default:
                        //res = res.Concat(LeerLateralesTechos(tipoVidrio)).ToList();
                        obj = LeerLateralesTechos(tipoVidrio, obj);
                        break;
                }
            }
            catch (Exception ex)
            {
                var hasdf = 1;
                var cdfja = hasdf + 1;
            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerLuneta(string tipoVidrio)
        {
            var res = new List<CaracteristicaEurocode>();
            res.Add(new CaracteristicaEurocode(_lunetas[_eurosplit[7].ToString()], _eurosplit[7].ToString(), "TipoCarroceria"));
            try
            {
                res = res.Concat(LeerCaracteristicasLuneta()).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        private ArticuloEurocode LeerLuneta(string tipoVidrio, ArticuloEurocode obj)
        {
            var res = new List<CaracteristicaEurocode>();
            obj.Carroceria = _lunetas[_eurosplit[7].ToString()];
            obj.CaracterCarroceria = _eurosplit[7].ToString();
            obj.PosicionCaracterCarroceria = 7;
            //res.Add(new CaracteristicaEurocode(_lunetas[_eurosplit[7].ToString()], _eurosplit[7].ToString(), "TipoCarroceria"));
            try
            {
                //res = res.Concat(LeerCaracteristicasLuneta()).ToList();
                obj = LeerCaracteristicasLuneta(obj);
            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerCaracteristicasLuneta()
        {
            var res = new List<CaracteristicaEurocode>();
            //res.Add(new CaracteristicaEurocode(_lunetas[_eurosplit[7].ToString()], _eurosplit[7].ToString(), "TipoCarroceria"));

            var puntero = 8;
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "6":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            res.Add(new CaracteristicaEurocode(_lunetascaracter[caracteres], caracteres, "CaracteristicasLunetas"));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            res.Add(new CaracteristicaEurocode(_lunetascaracter[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasLunetas"));
                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "6":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return res;
        }
        
        private ArticuloEurocode LeerCaracteristicasLuneta(ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            //res.Add(new CaracteristicaEurocode(_lunetas[_eurosplit[7].ToString()], _eurosplit[7].ToString(), "TipoCarroceria"));
            obj.Caracteristicas = new List<CaracteristicasArticulo>();
            obj.Modificaciones = new List<CaracteristicasArticulo>();
            var puntero = 8;
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "6":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            obj.Modificaciones.Add(new CaracteristicasArticulo(_lunetascaracter[caracteres], caracteres));
                            //res.Add(new CaracteristicaEurocode(_lunetascaracter[caracteres], caracteres, "CaracteristicasLunetas"));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            obj.Caracteristicas.Add(new CaracteristicasArticulo(_lunetascaracter[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString()));
                            //res.Add(new CaracteristicaEurocode(_lunetascaracter[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasLunetas"));
                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "6":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerParabrisas(string tipoVidrio)
        {
            var res = new List<CaracteristicaEurocode>();
            var caracteres = _eurosplit[7].ToString() + _eurosplit[8].ToString();
            try
            {
                var tin = _toptins[caracteres];
                res.Add(new CaracteristicaEurocode(tin, caracteres, "TopTins"));
                res = res.Concat(LeerCaracteristicasParabrisas(9)).ToList();
            }
            catch (Exception ex)
            {
                res = res.Concat(LeerCaracteristicasParabrisas(7)).ToList();
            }


            return res;
        }
        private ArticuloEurocode LeerParabrisas(string tipoVidrio, ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            var caracteres = _eurosplit[7].ToString() + _eurosplit[8].ToString();
            try
            {
                var tin = _toptins[caracteres];
                obj.BandaSuperior = tin;
                obj.CaracterBandaSuperior = caracteres;
                //res.Add(new CaracteristicaEurocode(tin, caracteres, "TopTins"));
                obj = LeerCaracteristicasParabrisas(9, obj);
            }
            catch (Exception ex)
            {
                obj = LeerCaracteristicasParabrisas(7, obj);
            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerCaracteristicasParabrisas(int puntero)
        {
            var res = new List<CaracteristicaEurocode>();
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            res.Add(new CaracteristicaEurocode(_parabrisascaracter[caracteres], caracteres, "CaracteristicasParabrisas"));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            res.Add(new CaracteristicaEurocode(_parabrisascaracter[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasParabrisas"));
                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return res;
        }
        private ArticuloEurocode LeerCaracteristicasParabrisas(int puntero, ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            obj.Caracteristicas = new List<CaracteristicasArticulo>();
            obj.Modificaciones = new List<CaracteristicasArticulo>();
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            //res.Add(new CaracteristicaEurocode(_parabrisascaracter[caracteres], caracteres, "CaracteristicasParabrisas"));


                            obj.Modificaciones.Add(new CaracteristicasArticulo(_parabrisascaracter[caracteres], caracteres));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            //res.Add(new CaracteristicaEurocode(_parabrisascaracter[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasParabrisas"));

                            obj.Caracteristicas.Add(new CaracteristicasArticulo(_parabrisascaracter[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString()));
                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return obj;
        }
        private ArticuloEurocode LeerLateralesTechos(string tipoVidrio, ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            var caracteres = _eurosplit[7].ToString() + _eurosplit[8].ToString();
            //res.Add(new CaracteristicaEurocode(_carroceriavehiculo[caracteres], caracteres, "TipoCarroceria"));
            obj.Carroceria = _carroceriavehiculo[caracteres];
            obj.CaracterCarroceria = caracteres;
            try
            {
                //res = res.Concat(LeerPosicionVidrio()).ToList();
                obj = LeerPosicionVidrio(obj);
            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerLateralesTechos(string tipoVidrio)
        {
            var res = new List<CaracteristicaEurocode>();
            var caracteres = _eurosplit[7].ToString() + _eurosplit[8].ToString();
            res.Add(new CaracteristicaEurocode(_carroceriavehiculo[caracteres], caracteres, "TipoCarroceria"));
            try
            {
                res = res.Concat(LeerPosicionVidrio()).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        private List<CaracteristicaEurocode> LeerPosicionVidrio()
        {
            var res = new List<CaracteristicaEurocode>();
            var caracteres = _eurosplit[9].ToString() + _eurosplit[10].ToString();
            res.Add(new CaracteristicaEurocode(_posicionvidrio[caracteres], caracteres, "PosicionVidrio"));
            try
            {
                res = res.Concat(LeerCaracteristicasLateralesTechos()).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        private ArticuloEurocode LeerPosicionVidrio(ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            var caracteres = _eurosplit[9].ToString() + _eurosplit[10].ToString();
            //res.Add(new CaracteristicaEurocode(_posicionvidrio[caracteres], caracteres, "PosicionVidrio"));
            obj.PosicionVidrio = _posicionvidrio[caracteres];
            obj.CaracterPosicionVidrio = caracteres;
            try
            {
                //res = res.Concat(LeerCaracteristicasLateralesTechos()).ToList();
                obj = LeerCaracteristicasLateralesTechos(obj);
            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerCaracteristicasLateralesTechos()
        {
            var res = new List<CaracteristicaEurocode>();
            var puntero = 11;
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            res.Add(new CaracteristicaEurocode(_lateralestechoscaracter[caracteres], caracteres, "CaracteristicasLateralesTechos"));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            res.Add(new CaracteristicaEurocode(_lateralestechoscaracter[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasLateralesTechos"));
                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return res;
        }
        private ArticuloEurocode LeerCaracteristicasLateralesTechos(ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            obj.Caracteristicas = new List<CaracteristicasArticulo>();
            obj.Modificaciones = new List<CaracteristicasArticulo>();
            var puntero = 11;
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            //res.Add(new CaracteristicaEurocode(_lateralestechoscaracter[caracteres], caracteres, "CaracteristicasLateralesTechos"));

                            obj.Modificaciones.Add(new CaracteristicasArticulo(_lateralestechoscaracter[caracteres], caracteres));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            //res.Add(new CaracteristicaEurocode(_lateralestechoscaracter[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasLateralesTechos"));

                            obj.Caracteristicas.Add(new CaracteristicasArticulo(_lateralestechoscaracter[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString()));
                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerAccesorioSK()
        {
            var res = new List<CaracteristicaEurocode>();
            res.Add(new CaracteristicaEurocode(_tipoaccesoriosk[_eurosplit[6].ToString()], _eurosplit[6].ToString(), "TipoAccesorio"));
            res.Add(new CaracteristicaEurocode(_lunetas[_eurosplit[7].ToString()], _eurosplit[7].ToString(), "TipoCarroceria"));
            try
            {
                var caracteres = _eurosplit[8].ToString() + _eurosplit[9].ToString();
                res.Add(new CaracteristicaEurocode(_posicionvidrio[caracteres], caracteres, "PosicionVidrio"));
            }
            catch (Exception ex)
            {

            }


            try
            {
                res = res.Concat(LeerCaracteristicasAccesorioSK()).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        private ArticuloEurocode LeerAccesorioSK(ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            //res.Add(new CaracteristicaEurocode(_tipoaccesoriosk[_eurosplit[6].ToString()], _eurosplit[6].ToString(), "TipoAccesorio"));
            obj.TipoAccesorio = _tipoaccesoriosk[_eurosplit[6].ToString()];
            obj.CaracterTipoAccesorio = _eurosplit[6].ToString();
            //res.Add(new CaracteristicaEurocode(_lunetas[_eurosplit[7].ToString()], _eurosplit[7].ToString(), "TipoCarroceria"));
            obj.Carroceria = _lunetas[_eurosplit[7].ToString()];
            obj.CaracterCarroceria = _eurosplit[7].ToString();
            try
            {
                var caracteres = _eurosplit[8].ToString() + _eurosplit[9].ToString();
                obj.PosicionVidrio = _posicionvidrio[caracteres];
                obj.CaracterPosicionVidrio = caracteres;
                //res.Add(new CaracteristicaEurocode(_posicionvidrio[caracteres], caracteres, "PosicionVidrio"));
            }
            catch (Exception ex)
            {

            }


            try
            {
                //res = res.Concat(LeerCaracteristicasAccesorioSK()).ToList();
                obj = LeerCaracteristicasAccesorioSK(obj);
            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerAccesorioX()
        {
            var res = new List<CaracteristicaEurocode>();
            res.Add(new CaracteristicaEurocode(_tipoaccesoriox[_eurosplit[6].ToString()], _eurosplit[6].ToString(), "TipoAccesorio"));
            res.Add(new CaracteristicaEurocode(_lunetas[_eurosplit[7].ToString()], _eurosplit[7].ToString(), "TipoCarroceria"));
            try
            {
                var caracteres = _eurosplit[8].ToString() + _eurosplit[9].ToString();
                res.Add(new CaracteristicaEurocode(_posicionvidrio[caracteres], caracteres, "PosicionVidrio"));
            }
            catch (Exception ex)
            {

            }
            try
            {
                res = res.Concat(LeerCaracteristicasAccesorioX()).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        private ArticuloEurocode LeerAccesorioX(ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            //res.Add(new CaracteristicaEurocode(_tipoaccesoriox[_eurosplit[6].ToString()], _eurosplit[6].ToString(), "TipoAccesorio"));

            obj.TipoAccesorio = _tipoaccesoriox[_eurosplit[6].ToString()];
            obj.CaracterTipoAccesorio = _eurosplit[6].ToString();

            //res.Add(new CaracteristicaEurocode(_lunetas[_eurosplit[7].ToString()], _eurosplit[7].ToString(), "TipoCarroceria"));

            obj.Carroceria = _lunetas[_eurosplit[7].ToString()];
            obj.CaracterCarroceria = _eurosplit[7].ToString();

            try
            {
                var caracteres = _eurosplit[8].ToString() + _eurosplit[9].ToString();
                //res.Add(new CaracteristicaEurocode(_posicionvidrio[caracteres], caracteres, "PosicionVidrio"));

                obj.PosicionVidrio = _posicionvidrio[caracteres];
                obj.CaracterPosicionVidrio = caracteres;
            }
            catch (Exception ex)
            {

            }
            try
            {
                //res = res.Concat(LeerCaracteristicasAccesorioX()).ToList();
                obj = LeerCaracteristicasAccesorioX(obj);

            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerCaracteristicasAccesorioX()
        {
            var res = new List<CaracteristicaEurocode>();
            var puntero = 10;
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            res.Add(new CaracteristicaEurocode(_caracteristicasaccesoriosx[caracteres], caracteres, "CaracteristicasAccesoriosX"));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            res.Add(new CaracteristicaEurocode(_caracteristicasaccesoriosx[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasAccesoriosX"));
                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return res;
        }
        private ArticuloEurocode LeerCaracteristicasAccesorioX(ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            var puntero = 10;
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            //res.Add(new CaracteristicaEurocode(_caracteristicasaccesoriosx[caracteres], caracteres, "CaracteristicasAccesoriosX"));
                            obj.Modificaciones.Add(new CaracteristicasArticulo(_caracteristicasaccesoriosx[caracteres], caracteres));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            //res.Add(new CaracteristicaEurocode(_caracteristicasaccesoriosx[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasAccesoriosX"));
                            obj.Caracteristicas.Add(new CaracteristicasArticulo(_caracteristicasaccesoriosx[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString()));

                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return obj;
        }
        private List<CaracteristicaEurocode> LeerCaracteristicasAccesorioSK()
        {
            var res = new List<CaracteristicaEurocode>();
            var puntero = 10;
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            res.Add(new CaracteristicaEurocode(_caracteristicasaccesoriossk[caracteres], caracteres, "CaracteristicasAccesoriosSK"));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            res.Add(new CaracteristicaEurocode(_caracteristicasaccesoriossk[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasAccesoriosSK"));
                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return res;
        }
        private ArticuloEurocode LeerCaracteristicasAccesorioSK(ArticuloEurocode obj)
        {
            //var res = new List<CaracteristicaEurocode>();
            obj.Caracteristicas = new List<CaracteristicasArticulo>();
            obj.Modificaciones = new List<CaracteristicasArticulo>();
            var puntero = 10;
            while (puntero < _eurosplit.Count)
            {
                try
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            var caracteres = _eurosplit[puntero].ToString() + _eurosplit[puntero + 1].ToString();
                            //res.Add(new CaracteristicaEurocode(_caracteristicasaccesoriossk[caracteres], caracteres, "CaracteristicasAccesoriosSK"));
                            obj.Modificaciones.Add(new CaracteristicasArticulo(_caracteristicasaccesoriossk[caracteres], caracteres));
                            puntero++;
                            puntero++;
                            break;
                        default:
                            //res.Add(new CaracteristicaEurocode(_caracteristicasaccesoriossk[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString(), "CaracteristicasAccesoriosSK"));
                            obj.Caracteristicas.Add(new CaracteristicasArticulo(_caracteristicasaccesoriossk[_eurosplit[puntero].ToString()], _eurosplit[puntero].ToString()));
                            puntero++;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    switch (_eurosplit[puntero].ToString())
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            puntero++;
                            puntero++;
                            break;
                        default:
                            puntero++;
                            break;
                    }
                }

            }
            return obj;
        }
    }
}
