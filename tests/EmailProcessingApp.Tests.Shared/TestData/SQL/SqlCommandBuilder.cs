namespace EmailProcessingApp.Tests.Shared.TestData.SQL
{
    public static class SqlCommandBuilder
    {
        public static string Insert<T>(T entity, string tableName)
        {
            string command = $@"INSERT INTO ""{tableName}"" (";

            var type = typeof(T);
            var properties = type.GetProperties()
                .Where(pi => pi.PropertyType.IsPrimitive
                             || pi.PropertyType == typeof(string) || pi.PropertyType == typeof(Guid) ||
                             pi.PropertyType == typeof(Guid?) || pi.PropertyType == typeof(DateTime)).ToList();

            int currentPropLoopIndex = 1;

            foreach (var prop in properties)
            {
                if (currentPropLoopIndex == properties.Count)
                {
                    command += $@"""{prop.Name}"") VALUES (";
                }
                else if (currentPropLoopIndex < properties.Count)
                {
                    command += $@"""{prop.Name}"", ";
                }

                currentPropLoopIndex += 1;
            }

            int currentPropValueLoopIndex = 1;

            foreach (var prop in properties)
            {
                var value = prop.GetValue(entity);
                if (value != null)
                {
                    if (currentPropValueLoopIndex == properties.Count)
                    {
                        if (value is string || value is Guid || value is Guid?)
                        {
                            command += $"'{prop.GetValue(entity)}');";
                        }
                        else if (value is DateTime)
                        {
                            var formattedDate = string.Format("{0:yyyy-MM-ddTHH:mm:ss.FFFZ}", value);
                            command += $"'{formattedDate}');";
                        }
                        else
                        {
                            command += $"{prop.GetValue(entity)});";
                        }
                    }
                    else if (currentPropValueLoopIndex < properties.Count)
                    {
                        if (value is string || value is Guid || value is Guid?)
                        {
                            command += $"'{prop.GetValue(entity)}', ";
                        }
                        else if (value is DateTime)
                        {
                            var formattedDate = string.Format("{0:yyyy-MM-ddTHH:mm:ss.FFFZ}", value);
                            command += $"'{formattedDate}', ";
                        }
                        else
                        {
                            command += $"{prop.GetValue(entity)}, ";
                        }
                    }
                }

                currentPropValueLoopIndex += 1;
            }

            return command;
        }
    }
}
