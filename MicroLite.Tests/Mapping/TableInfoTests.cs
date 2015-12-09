﻿namespace MicroLite.Tests.Mapping
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data;
    using MicroLite.FrameworkExtensions;
    using MicroLite.Mapping;
    using MicroLite.Tests.TestEntities;
    using Xunit;

    /// <summary>
    /// Unit Tests for the <see cref="TableInfo" /> class.
    /// </summary>
    public class TableInfoTests
    {
        [Fact]
        public void ConstructorSetsPropertyValuesWithIdentifierMapped()
        {
            var columns = new ReadOnlyCollection<ColumnInfo>(new[]
            {
                new ColumnInfo("Name", DbType.String, typeof(Customer).GetProperty("Name"), false, true, true, null),
                new ColumnInfo("CustomerId", DbType.Int32, typeof(Customer).GetProperty("Id"), true, true, false, null),
                new ColumnInfo("Created", DbType.DateTime, typeof(Customer).GetProperty("Created"), false, true, false, null),
                new ColumnInfo("Updated", DbType.DateTime, typeof(Customer).GetProperty("Updated"), false, false, true, null)
            });
            var identifierStrategy = IdentifierStrategy.Assigned;
            var name = "Customers";
            var schema = "Sales";

            var tableInfo = new TableInfo(columns, identifierStrategy, name, schema);

            Assert.Equal(columns, tableInfo.Columns);
            Assert.Equal(columns[1].ColumnName, tableInfo.IdentifierColumn.ColumnName);
            Assert.Equal(identifierStrategy, tableInfo.IdentifierStrategy);
            Assert.Equal(name, tableInfo.Name);
            Assert.Equal(schema, tableInfo.Schema);
            Assert.Equal(3, tableInfo.InsertColumnCount);
            Assert.Equal(2, tableInfo.UpdateColumnCount);
        }

        [Fact]
        public void ConstructorSetsPropertyValuesWithoutIdentifierMapped()
        {
            var columns = new ReadOnlyCollection<ColumnInfo>(new[]
            {
                new ColumnInfo("Name", DbType.String, typeof(Customer).GetProperty("Name"), false, true, true, null),
                new ColumnInfo("Created", DbType.DateTime, typeof(Customer).GetProperty("Created"), false, true, false, null),
                new ColumnInfo("Updated", DbType.DateTime, typeof(Customer).GetProperty("Updated"), false, false, true, null)
            });
            var identifierStrategy = IdentifierStrategy.Assigned;
            var name = "Customers";
            var schema = "Sales";

            var tableInfo = new TableInfo(columns, identifierStrategy, name, schema);

            Assert.Equal(columns, tableInfo.Columns);
            Assert.Null(tableInfo.IdentifierColumn);
            Assert.Equal(identifierStrategy, tableInfo.IdentifierStrategy);
            Assert.Equal(name, tableInfo.Name);
            Assert.Equal(schema, tableInfo.Schema);
            Assert.Equal(2, tableInfo.InsertColumnCount);
            Assert.Equal(2, tableInfo.UpdateColumnCount);
        }

        [Fact]
        public void ConstructorThrowsArgumentNullExceptionForNullColumns()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => new TableInfo(columns: null, identifierStrategy: IdentifierStrategy.DbGenerated, name: "Customers", schema: "Sales"));

            Assert.Equal("columns", exception.ParamName);
        }

        [Fact]
        public void ConstructorThrowsArgumentNullExceptionForNullName()
        {
            var exception = Assert.Throws<ArgumentNullException>(
                () => new TableInfo(columns: new ColumnInfo[0], identifierStrategy: IdentifierStrategy.DbGenerated, name: null, schema: "Sales"));

            Assert.Equal("name", exception.ParamName);
        }

        [Fact]
        public void ConstructorThrowsMappingExceptionIfIdentifierStrategySequenceButSequenceNameIsNull()
        {
            var columns = new[]
            {
                new ColumnInfo("Id", DbType.Int32, typeof(Customer).GetProperty("Id"), true, true, true, null)
            };

            var exception = Assert.Throws<MappingException>(
                () => new TableInfo(columns: columns, identifierStrategy: IdentifierStrategy.Sequence, name: "Customers", schema: "Sales"));

            Assert.Equal(ExceptionMessages.TableInfo_SequenceNameNotSet.FormatWith("Id"), exception.Message);
        }

        [Fact]
        public void ConstructorThrowsMappingExceptionIfMultipleColumnsWithSameName()
        {
            var columns = new[]
            {
                new ColumnInfo("Name", DbType.String, typeof(Customer).GetProperty("Name"), false, true, true, null),
                new ColumnInfo("Name", DbType.String, typeof(Customer).GetProperty("Name"), false, true, true, null)
            };

            var exception = Assert.Throws<MappingException>(
                () => new TableInfo(columns: columns, identifierStrategy: IdentifierStrategy.DbGenerated, name: "Customers", schema: "Sales"));

            Assert.Equal(ExceptionMessages.TableInfo_ColumnMappedMultipleTimes.FormatWith("Name"), exception.Message);
        }

        [Fact]
        public void ConstructorThrowsMappingExceptionMultipleColumnsAreIdentifierColumn()
        {
            var columns = new[]
            {
                new ColumnInfo("CustomerId", DbType.Int32, typeof(Customer).GetProperty("Id"), true, true, true, null),
                new ColumnInfo("Id", DbType.Int32, typeof(Customer).GetProperty("Id"), true, true, true, null)
            };

            var exception = Assert.Throws<MappingException>(
                () => new TableInfo(columns: columns, identifierStrategy: IdentifierStrategy.DbGenerated, name: "Customers", schema: "Sales"));

            Assert.Equal(ExceptionMessages.TableInfo_MultipleIdentifierColumns.FormatWith("Sales", "Customers"), exception.Message);
        }
    }
}