using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;


namespace CodeGenerator
{
    public class SqlDatabaseAdapter
    {
        private readonly ServerConnection _serverConnection;

        public SqlDatabaseAdapter(string connectionString)
        {
            _serverConnection = new ServerConnection(new SqlConnection(connectionString));
        }

        public List<string> GetTables(string databaseName)
        {
            List<string> list = new List<string>();

            var server = new Server(_serverConnection);
            var database = server.Databases.Cast<Database>().FirstOrDefault(d => d.Name == databaseName);

            var dataTable = new List<string>();
            if (database == null) return dataTable;

            foreach (var table in database.Tables.Cast<Table>())
            {
                dataTable.Add(table.Name);
            }

            return dataTable;
        }


        public DataSet GetTablesWithColumn(string databaseName = "master")
        {
            var server = new Server(_serverConnection);
            var database = server.Databases.Cast<Database>().FirstOrDefault(d => d.Name == databaseName);

            var dataSet = new DataSet(databaseName);
            if (database == null) return dataSet;

            foreach (var table in database.Tables.Cast<Table>())
            {
                var dataTable = new DataTable(table.Name);

                FillColumns(table, dataTable);

                dataSet.Tables.Add(dataTable);
            }

            return dataSet;
        }

        public string GetTableDescription(string databaseName, string _table)
        {
            var server = new Server(_serverConnection);
            var database = server.Databases.Cast<Database>().FirstOrDefault(d => d.Name == databaseName);
            if (database == null) return "";

            var table = database.Tables.Cast<Table>().FirstOrDefault(f => f.Name == _table);
            if (table == null) return "";

            //return table.Properties["Discription"].ToString();
            string Description = "";
            try
            {
                Description = table.ExtendedProperties["MS_Description"].Value.ToString();
            }
            catch
            {
                Description = "";
            }
            return Description;
        }

        public DataTable GetTablesColumn(string tableName, string databaseName = "master")
        {
            var server = new Server(_serverConnection);
            var database = server.Databases.Cast<Database>().FirstOrDefault(d => d.Name == databaseName);

            var dataTable = new DataTable(tableName);

            if (database == null) return dataTable;

            database.Refresh();

            var table = database.Tables.Cast<Table>().FirstOrDefault(f => f.Name == tableName);

            if (table == null) return dataTable;

            FillColumns(table, dataTable);

            return dataTable;
        }

        private static void FillColumns(Table table, DataTable dataTable)
        {

            var dataName = new DataColumn("Name"); //column.Properties[""].ToString();
            var dataType = new DataColumn("Type");
            var dataDescription = new DataColumn("Description");

            dataTable.Columns.Add(dataName);
            dataTable.Columns.Add(dataType);
            dataTable.Columns.Add(dataDescription);

            foreach (Column column in table.Columns)
            {
                string PrimaryType = column.DataType.SqlDataType.ToString();
                var type = ConvertToClrType(column.DataType.SqlDataType, column.Nullable);

                DataRow dr = dataTable.NewRow();
                dr["Name"] = column.Name;
                dr["Type"] = type.Name.Contains("Nullable") ? ConvertToClrType(PrimaryType).Name : type.Name;
                //dr["Description"] = column.Properties["Description"].ToString();
                try
                {
                    dr["Description"] = column.ExtendedProperties["MS_Description"].Value.ToString();
                }
                catch
                {
                    dr["Description"] = "";
                }
                //string dsdfdfs = table.Columns[2].ExtendedProperties["MS_Description"].Value.ToString();
                dataTable.Rows.Add(dr);

            }
        }

        private static Type ConvertToClrType(SqlDataType sqlDataType, bool nullable)
        {
            switch (sqlDataType)
            {
                case SqlDataType.BigInt:
                    //return nullable ? typeof(long?) : typeof(long);
                    return nullable ? typeof(long?) : typeof(Int64);

                case SqlDataType.Binary:
                case SqlDataType.Image:
                case SqlDataType.Timestamp:
                case SqlDataType.VarBinary:
                case SqlDataType.VarBinaryMax:
                    return typeof(byte[]);

                case SqlDataType.Bit:
                    return nullable ? typeof(bool?) : typeof(bool);

                case SqlDataType.Char:
                case SqlDataType.NChar:
                case SqlDataType.NText:
                case SqlDataType.NVarChar:
                case SqlDataType.NVarCharMax:
                case SqlDataType.Text:
                case SqlDataType.VarChar:
                case SqlDataType.VarCharMax:
                case SqlDataType.Xml:
                    return typeof(string);

                case SqlDataType.DateTime:
                case SqlDataType.DateTime2:
                case SqlDataType.SmallDateTime:
                case SqlDataType.Date:
                case SqlDataType.Time:
                    return nullable ? typeof(DateTime?) : typeof(DateTime);

                case SqlDataType.Decimal:
                case SqlDataType.Money:
                case SqlDataType.SmallMoney:
                case SqlDataType.Numeric:
                    return nullable ? typeof(decimal?) : typeof(decimal);

                case SqlDataType.Float:
                    return nullable ? typeof(double?) : typeof(double);

                case SqlDataType.Int:
                    //return nullable ? typeof(int?) : typeof(int);
                    return nullable ? typeof(int?) : typeof(Int32);

                case SqlDataType.Real:
                    return nullable ? typeof(float?) : typeof(float);

                case SqlDataType.UniqueIdentifier:
                    return nullable ? typeof(Guid?) : typeof(Guid);

                case SqlDataType.SmallInt:
                    //return nullable ? typeof(short?) : typeof(short);
                    return nullable ? typeof(short?) : typeof(Int16);

                case SqlDataType.TinyInt:
                    return typeof(byte?);

                case SqlDataType.Variant:
                    return typeof(object);

                case SqlDataType.DateTimeOffset:
                    return nullable ? typeof(DateTimeOffset?) : typeof(DateTimeOffset);

                default:
                    throw new ArgumentOutOfRangeException("sqlDataType");
            }
        }
        private static Type ConvertToClrType(string sqlDataType)
        {
            switch (sqlDataType)
            {
                case "BigInt":
                    //return nullable ? typeof(long?) ": typeof(long);
                    //return nullable ? typeof(long?) ": typeof(Int64);
                    return typeof(Int64);

                case "Binary":
                case "Image":
                case "Timestamp":
                case "VarBinary":
                case "VarBinaryMax":
                    return typeof(byte[]);

                case "Bit":
                    //return nullable ? typeof(bool?) ": typeof(bool);
                    return typeof(bool);

                case "Char":
                case "NChar":
                case "NText":
                case "NVarChar":
                case "NVarCharMax":
                case "Text":
                case "VarChar":
                case "VarCharMax":
                case "Xml":
                    return typeof(string);

                case "DateTime":
                case "DateTime2":
                case "SmallDateTime":
                case "Date":
                case "Time":
                    //return nullable ? typeof(DateTime?) ": typeof(DateTime);
                    return typeof(DateTime);

                case "Decimal":
                case "Money":
                case "SmallMoney":
                case "Numeric":
                    //return nullable ? typeof(decimal?) ": typeof(decimal);
                    return typeof(decimal);

                case "Float":
                    //return nullable ? typeof(double?) ": typeof(double);
                    return typeof(double);

                case "Int":
                    //return nullable ? typeof(int?) ": typeof(int);
                    //return nullable ? typeof(int?) ": typeof(Int32);
                    return typeof(Int32);

                case "Real":
                    //return nullable ? typeof(float?) ": typeof(float);
                    return typeof(float);

                case "UniqueIdentifier":
                    //return nullable ? typeof(Guid?) ": typeof(Guid);
                    return typeof(Guid);

                case "SmallInt":
                    //return nullable ? typeof(short?) ": typeof(short);
                    //return nullable ? typeof(short?) ": typeof(Int16);
                    return typeof(Int16);

                case "TinyInt":
                    return typeof(byte);

                case "Variant":
                    return typeof(object);

                case "DateTimeOffset":
                    //return nullable ? typeof(DateTimeOffset?) ": typeof(DateTimeOffset);
                    return typeof(DateTimeOffset);

                default:
                    throw new ArgumentOutOfRangeException("sqlDataType");
            }
        }
    }
}
