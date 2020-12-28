﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class NotasCD
    {
        // Crear
        public int Crear(NotasCE notasCE)
        {
            // Establecer conexion
            SqlConnection cn = ConexionCD.conectarBD();

            // Abrir conexion
            cn.Open();

            // Crear comando
            SqlCommand cmd = cn.CreateCommand();

            // Definir tipo de comando
            cmd.CommandType = CommandType.Text;

            // Definir consulta
            cmd.CommandText = "insert Notas " +
                "into (idEstudiante, idEvaluacion, nota) " +
                "values (@idEstudiante, @idEvaluacion, @nota)";

            // Agregar parametros
            cmd.Parameters.AddWithValue("@idEstudiante", notasCE.IdEstudiante);
            cmd.Parameters.AddWithValue("@idEvaluacion", notasCE.IdEvaluacion);
            cmd.Parameters.AddWithValue("@nota", notasCE.Nota);

            // Ejecutar comando
            int numFila = cmd.ExecuteNonQuery();

            // Crear id nuevo
            int nuevoID;

            if (numFila > 0)
            {
                cmd.CommandText = "select max(id) as nuevoId from Notas where idEstudiante = @nota";
                cmd.Parameters["@nota"].Value = notasCE.Nota;

                // ejecutar consulta
                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    nuevoID = (int)dataReader["nuevoId"];
                }
                else
                {
                    nuevoID = 0;
                }
            }
            else
            {
                nuevoID = 0;
            }

            // Cerramos la conexion
            cn.Close();

            // Retornamos nuevo id
            return nuevoID;
        }

        // Leer
        public List<NotasCE> Leer()
        {
            // establecer conexion
            SqlConnection cn = ConexionCD.conectarBD();

            // Abrir conexion
            cn.Open();

            // Crear comando
            SqlCommand cmd = cn.CreateCommand();

            // definir tipo
            cmd.CommandType = CommandType.Text;

            // Definir consulta
            cmd.CommandText = "select * from Notas";

            // ejecutar consulta
            SqlDataReader dataReader = cmd.ExecuteReader();

            // Crear lista
            List<NotasCE> notasCEs = new List<NotasCE>();

            while (dataReader.Read())
            {
                int id = (int)dataReader["id"];
                int idEstudiante = (int)dataReader["idEstudiante"];
                int idEvaluacion = (int)dataReader["idEvaluacion"];
                double nota = (double)dataReader["nota"];

                NotasCE notasCE = new NotasCE(id, idEstudiante, idEvaluacion, nota);

                notasCEs.Add(notasCE);
            }
            // cerrar conexion
            cn.Close();

            // retornar lista
            return notasCEs;
        }

        // Actualizar
        public int Actualizar(NotasCE notasCE)
        {
            // Establecer conexion
            SqlConnection cn = ConexionCD.conectarBD();

            // Abrir conexion
            cn.Open();

            // Crear comando
            SqlCommand cmd = cn.CreateCommand();

            // Definir tipo de comando
            cmd.CommandType = CommandType.Text;

            // Definir consulta
            cmd.CommandText = "Update Notas" +
                " set idEstudiante = @idEstudiante, idEvaluacion = @idEvaluacion, nota = @nota " +
                "where id = @id";

            // Agregar parametros
            cmd.Parameters.AddWithValue("@idEstudiante", notasCE.IdEstudiante);
            cmd.Parameters.AddWithValue("@idEvaluacion", notasCE.IdEvaluacion);
            cmd.Parameters.AddWithValue("@nota", notasCE.Nota);
            cmd.Parameters.AddWithValue("@id", notasCE.Id);

            // Ejecutar comando
            int numFila = cmd.ExecuteNonQuery();

            
            // Cerramos la conexion
            cn.Close();

            // Retornamos nuevo id
            return numFila;
        }

        // Eliminar
        public int Eliminar(NotasCE notasCE)
        {
            // Establecer conexion
            SqlConnection cn = ConexionCD.conectarBD();

            // Abrir conexion
            cn.Open();

            // Crear comando
            SqlCommand cmd = cn.CreateCommand();

            // Definir tipo de comando
            cmd.CommandType = CommandType.Text;

            // Definir consulta
            cmd.CommandText = "Delete from Notas where id = @id";

            // Agregar parametros
            cmd.Parameters.AddWithValue("@id", notasCE.Id);

            // Ejecutar comando
            int numFila = cmd.ExecuteNonQuery();


            // Cerramos la conexion
            cn.Close();

            // Retornamos nuevo id
            return numFila;
        }
    }
}