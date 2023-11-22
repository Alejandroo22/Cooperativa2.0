using MySql.Data.MySqlClient;
using sistema_modular_cafe_majada.model.Connection;
using sistema_modular_cafe_majada.model.Mapping.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_modular_cafe_majada.model.DAO
{
    class ReportesDAO
    {
        private ConnectionDB conexion;

        public ReportesDAO()
        {
            //Se crea la instancia de la clase conexion
            conexion = new ConnectionDB();
        }
        public List<ReportesCCaliadades> GetCCalidadData(int cosecha)
        {
            List<ReportesCCaliadades> data = new List<ReportesCCaliadades>();

            try
            {
                conexion.Conectar();
                string consulta = @"
                                    SELECT
                                        a.id_calidad_cafe AS IdCalidad,
                                        c.nombre_calidad AS calidad_cafe,
                                        sp.nombre_subproducto AS subproducto,
                                        CONCAT(b.nombre_bodega, ' - ', a.nombre_almacen) AS almacenado_en,
                                        SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Entrada%' THEN ccsp.cantidad_saco_cafe ELSE 0 END) 
                                            + COALESCE((SELECT SUM(ccsp2.cantidad_saco_cafe)
                                                        FROM CantidadCafe_Silo_Piña ccsp2
                                                        WHERE ccsp2.id_almacen_silo_piña = a.id_almacen
                                                        AND ccsp2.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Destino%'), 0) 
                                            AS entradas_cantidad_sacos,
                                        SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Entrada%' THEN ccsp.cantidad_qqs_cafe ELSE 0 END) 
                                            + COALESCE((SELECT SUM(ccsp2.cantidad_qqs_cafe)
                                                        FROM CantidadCafe_Silo_Piña ccsp2
                                                        WHERE ccsp2.id_almacen_silo_piña = a.id_almacen
                                                        AND ccsp2.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Destino%'), 0) 
                                            AS entradas_cantidad_qqs,
                                        SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Salida%' THEN ccsp.cantidad_saco_cafe ELSE 0 END) AS salidas_cantidad_sacos,
                                        SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Salida%' THEN ccsp.cantidad_qqs_cafe ELSE 0 END) AS salidas_cantidad_qqs,
                                        SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Procedencia%' THEN ccsp.cantidad_saco_cafe ELSE 0 END) 
                                            AS traslados_cantidad_sacos_procedencia,
                                        SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Procedencia%' THEN ccsp.cantidad_qqs_cafe ELSE 0 END) 
                                            AS traslados_cantidad_qqs_procedencia
                                    FROM
                                        Almacen a
                                    JOIN
                                        Calidad_Cafe c ON a.id_calidad_cafe = c.id_calidad
                                    JOIN
                                        Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega
                                    JOIN
                                        SubProducto sp ON a.id_subproducto_cafe = sp.id_subproducto
                                    LEFT JOIN
                                        CantidadCafe_Silo_Piña ccsp ON ccsp.id_almacen_silo_piña = a.id_almacen
                                    WHERE
                                        ccsp.id_cosecha_cantidad = @id_cosecha
                                    GROUP BY
                                        a.id_calidad_cafe,
                                        c.nombre_calidad,
                                        sp.nombre_subproducto,
                                        almacenado_en,

                                        a.id_almacen
                                    ORDER BY
	                                    id_calidad_cafe,
	                                    nombre_calidad,
	                                    nombre_subproducto,
                                        almacenado_en;
            ";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id_cosecha", cosecha);
                MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta);

                while (reader.Read())
                {
                    ReportesCCaliadades reportesCCalidades = new ReportesCCaliadades()
                    {
                        id_nombre_calidad = Convert.ToInt32(reader["IdCalidad"]),
                        nombre_calidad = reader["calidad_cafe"].ToString(),
                        nombre_subproducto = reader["subproducto"].ToString(),
                        almacenado_en = reader["almacenado_en"].ToString(),
                        total_sacosE = Convert.ToDouble(reader["entradas_cantidad_sacos"]),
                        total_qqspuntoE = Convert.ToDouble(reader["entradas_cantidad_qqs"]),
                        total_sacosS = Convert.ToDouble(reader["salidas_cantidad_sacos"]),
                        total_qqspuntoS = Convert.ToDouble(reader["salidas_cantidad_qqs"]),
                        total_sacosT = Convert.ToDouble(reader["traslados_cantidad_sacos_procedencia"]),
                        total_qqspuntoT = Convert.ToDouble(reader["traslados_cantidad_qqs_procedencia"])
                    };
                    data.Add(reportesCCalidades);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte de calidades de cafe: " + ex.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return data;
        }
        public List<ReportesSubpartida> GetSubpartidaData(int cosecha, string fechaini, string fechafin)
        {
            List<ReportesSubpartida> data = new List<ReportesSubpartida>();

            try
            {
                conexion.Conectar();
                string consulta = @"
                                    SELECT
                                        sp.num_subpartida AS subpartida,
                                        sp.num1_semana_subpartida AS semana,
                                        dias1_subpartida AS partida,
                                        sp.fecha1_subpartida AS fecha,
                                        cc.nombre_calidad AS calidad_cafe,
                                        pd.nombre_procedencia AS procedencia,
                                        CONCAT(bodega.nombre_bodega, ' - ', al.nombre_almacen) AS almacenado_en,
                                        sp.peso_saco_subpartida AS sacos,
                                        sp.peso_qqs_subpartida AS qqs_punto,
                                        DATE_FORMAT(sp.inicio_secado_subpartida, '%d/%m/%Y') AS inicio_secado,
                                        DATE_FORMAT(sp.salida_punto_secado_subpartida, '%d/%m/%Y') AS fin_secado,
                                        ROUND(HOUR(sp.tiempo_secado_subpartida) + MINUTE(sp.tiempo_secado_subpartida) / 60, 1) AS tiempo,
                                        ROUND((sp.peso_qqs_subpartida / sp.rendimiento_subpartida), 2) AS qqs_oro,
                                        sp.humedad_secado_subpartida AS humedad,
                                        sp.rendimiento_subpartida AS rendimiento,
                                        per.nombre_personal AS puntero,
                                        DATE_FORMAT(sp.fecha_catacion_subpartida, '%d/%m/%Y') AS fecha_catacion,
                                        sp.observacion_catacion_subpartida AS catacion,
                                        sp.docto_almacen_subpartida AS almacen,
                                        DATE_FORMAT(sp.fecha_carga_secado_subpartida, '%d/%m/%Y') AS fecha_secado
                                    FROM
                                        SubPartida sp
                                    LEFT JOIN Calidad_Cafe cc ON sp.id_calidad_cafe_subpartida = cc.id_calidad
                                    LEFT JOIN Procedencia_Destino_Cafe pd ON sp.id_procedencia_subpartida = pd.id_procedencia
                                    LEFT JOIN Almacen al ON sp.id_almacen_subpartida = al.id_almacen
                                    LEFT JOIN Bodega_Cafe bodega ON sp.id_bodega_subpartida = bodega.id_bodega
                                    LEFT JOIN Personal per ON sp.id_puntero_secado_subpartida = per.id_personal
                                    WHERE
                                        sp.id_cosecha_subpartida = @id_cosecha AND 
                                        IF(
                                          fecha1_subpartida LIKE '%al%',
                                          STR_TO_DATE(CONCAT(SUBSTRING_INDEX(fecha1_subpartida, ' al ', 1), '/', SUBSTRING(fecha1_subpartida, LOCATE('al ', fecha1_subpartida) + 7)), '%d/%m/%Y'),
                                          STR_TO_DATE(fecha1_subpartida, '%d/%m/%Y')
                                        ) BETWEEN STR_TO_DATE(@fecha_inicial, '%d/%m/%Y') AND STR_TO_DATE(@fecha_final, '%d/%m/%Y');
            ";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id_cosecha", cosecha);
                conexion.AgregarParametro("@fecha_inicial", fechaini);
                conexion.AgregarParametro("@fecha_final", fechafin);
                //
                MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta);

                while (reader.Read())
                {
                    ReportesSubpartida reportesSubpartida = new ReportesSubpartida()
                    {
                        Subpartida = Convert.ToInt32(reader["subpartida"]),
                        Semana = Convert.ToInt32(reader["semana"]),
                        Partida = reader["partida"].ToString(),
                        Fecha = reader["fecha"].ToString(),
                        CalidadCafe = reader["calidad_cafe"].ToString(),
                        Procedencia = reader["procedencia"].ToString(),
                        AlmacenadoEn = reader["almacenado_en"].ToString(),
                        Sacos = Convert.ToDouble(reader["sacos"]),
                        QqsPunto = Convert.ToDouble(reader["qqs_punto"]),
                        InicioSecado = reader["inicio_secado"].ToString(),
                        FinSecado = reader["fin_secado"].ToString(),
                        Tiempo = reader["tiempo"].ToString(),
                        QqsOro = Convert.ToDouble(reader["qqs_oro"]),
                        Humedad = Convert.ToDouble(reader["humedad"]),
                        Rendimiento = Convert.ToDouble(reader["rendimiento"]),
                        Puntero = reader["puntero"].ToString(),
                        FechaCatacion = reader["fecha_catacion"].ToString(),
                        Catacion = reader["catacion"].ToString(),
                        Almacen = reader["almacen"].ToString(),
                        FechaSecado = reader["fecha_secado"].ToString()
                    };

                    data.Add(reportesSubpartida);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte de subpartida: " + ex.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return data;
        }
        public List<ReportesBodegas> GetBodegaData(int cosecha)
        {
            List<ReportesBodegas> data = new List<ReportesBodegas>();

            try
            {
                conexion.Conectar();
                string consulta = @"
                SELECT DISTINCT
                    TRIM(b.nombre_bodega) AS nombre_bodega,
                    c.nombre_calidad AS calidad_cafe,
                    a.cantidad_actual_saco_almacen AS total_sacos,
                    a.cantidad_actual_almacen AS total_qqspunto
                FROM
                    Almacen a
                JOIN
                    Calidad_Cafe c ON a.id_calidad_cafe = c.id_calidad
                JOIN
                    Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega
                JOIN
                    CantidadCafe_Silo_Piña ccsp ON ccsp.id_almacen_silo_piña = a.id_almacen
                WHERE
                    ccsp.id_cosecha_cantidad = @id_cosecha;

            ";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id_cosecha", cosecha);
                MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta);

                while (reader.Read())
                {
                    ReportesBodegas reportesBodegas = new ReportesBodegas()
                    {

                        nombre_bodega = reader["nombre_bodega"].ToString(),
                        calidad_cafe = reader["calidad_cafe"].ToString(),
                        total_sacos = Convert.ToDouble(reader["total_sacos"]),
                        total_qqspunto = Convert.ToDouble(reader["total_qqspunto"]),
                    };
                    data.Add(reportesBodegas);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte de subpartida: " + ex.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return data;
        }
        public List<ReportesCafeBodegas> GetCafeBodegaData(int cosecha)
        {
            List<ReportesCafeBodegas> data = new List<ReportesCafeBodegas>();

            try
            {
                conexion.Conectar();
                string consulta = @"
                SELECT
                    b.nombre_bodega,
                    a.nombre_almacen,
                    a.id_calidad_cafe,
                    c.nombre_calidad,
                    sp.nombre_subproducto,
                    SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Entrada%' THEN ccsp.cantidad_saco_cafe ELSE 0 END) 
                        + COALESCE((SELECT SUM(ccsp2.cantidad_saco_cafe)
                                    FROM CantidadCafe_Silo_Piña ccsp2
                                    WHERE ccsp2.id_almacen_silo_piña = a.id_almacen
                                    AND ccsp2.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Destino%'), 0) 
                        AS entradas_cantidad_sacos,
                    SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Entrada%' THEN ccsp.cantidad_qqs_cafe ELSE 0 END) 
                        + COALESCE((SELECT SUM(ccsp2.cantidad_qqs_cafe)
                                    FROM CantidadCafe_Silo_Piña ccsp2
                                    WHERE ccsp2.id_almacen_silo_piña = a.id_almacen
                                    AND ccsp2.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Destino%'), 0) 
                        AS entradas_cantidad_qqs,
                    SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Salida%' THEN ccsp.cantidad_saco_cafe ELSE 0 END) AS salidas_cantidad_sacos,
                    SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Salida%' THEN ccsp.cantidad_qqs_cafe ELSE 0 END) AS salidas_cantidad_qqs,
                    SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Procedencia%' THEN ccsp.cantidad_saco_cafe ELSE 0 END) 
                        AS traslados_cantidad_sacos_procedencia,
                    SUM(CASE WHEN ccsp.tipo_movimiento_cantidad_cafe LIKE '%Traslado Cafe - Procedencia%' THEN ccsp.cantidad_qqs_cafe ELSE 0 END) 
                        AS traslados_cantidad_qqs_procedencia
                FROM
                    Almacen a
                JOIN
                    Calidad_Cafe c ON a.id_calidad_cafe = c.id_calidad
                JOIN
                    Bodega_Cafe b ON a.id_bodega_ubicacion_almacen = b.id_bodega
                JOIN
                    SubProducto sp ON a.id_subproducto_cafe = sp.id_subproducto
                LEFT JOIN
                    CantidadCafe_Silo_Piña ccsp ON ccsp.id_almacen_silo_piña = a.id_almacen
                WHERE
                    ccsp.id_cosecha_cantidad = @id_cosecha
                GROUP BY
                    b.nombre_bodega,
                    a.nombre_almacen,
                    a.id_calidad_cafe,
                    c.nombre_calidad,
                    sp.nombre_subproducto,
                    a.id_almacen
                ORDER BY
                    b.nombre_bodega;
            ";

                conexion.CrearComando(consulta);
                conexion.AgregarParametro("@id_cosecha", cosecha);

                MySqlDataReader reader = conexion.EjecutarConsultaReader(consulta);

                while (reader.Read())
                {
                    ReportesCafeBodegas reportesCafeBodegas = new ReportesCafeBodegas()
                    {
                        
                        nombre_bodega = reader["nombre_bodega"].ToString(),
                        nombre_almacen = reader["nombre_almacen"].ToString(),
                        id_calidad = Convert.ToInt32(reader["id_calidad_cafe"]),
                        nombre_calidad = reader["nombre_calidad"].ToString(),
                        nombre_subproducto = reader["nombre_subproducto"].ToString(),
                        total_sacosE = Convert.ToDouble(reader["entradas_cantidad_sacos"]),
                        total_qqspuntoE = Convert.ToDouble(reader["entradas_cantidad_qqs"]),
                        total_sacosS = Convert.ToDouble(reader["salidas_cantidad_sacos"]),
                        total_qqspuntoS = Convert.ToDouble(reader["salidas_cantidad_qqs"]),
                        total_sacosT = Convert.ToDouble(reader["traslados_cantidad_sacos_procedencia"]),
                        total_qqspuntoT = Convert.ToDouble(reader["traslados_cantidad_qqs_procedencia"])
                    };
                    data.Add(reportesCafeBodegas);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos del reporte de cafe bodegas: " + ex.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return data;
        }

    }
}
