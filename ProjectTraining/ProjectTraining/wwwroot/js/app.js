
$.fn.dataTable.moment("DD/MM/YYYY");

$("#table1").DataTable({
    // Design Assets
    stateSave: true,
    autoWidth: true,
    // ServerSide Setups
    processing: true,
    serverSide: true,
    // Paging Setups
    paging: true,
    // Searching Setups
    searching: { regex: true },
    // Ajax Filter
    ajax: {
        url: "/Users/LoadTable",
        // type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: function (d) {
            return JSON.stringify(d);
        }
    },
    // Columns Setups
    columns: [
        { data: "id" },
        { data: "UserName" },
        { data: "Password" },
        { data: "CreateDate" },
        { data: "Role" },
        {
            data: "creationDate",
            render: function (data, type, row) {
                // If display or filter data is requested, format the date
                if (type === "display" || type === "filter") {
                    return moment(data).format("ddd DD/MM/YYYY HH:mm:ss");
                }
                // Otherwise the data type requested (`type`) is type detection or
                // sorting data, for which we want to use the raw date value, so just return
                // that, unaltered
                return data;
            }
        }
    ],
    // Column Definitions
    columnDefs: [
        { targets: "no-sort", orderable: false },
        { targets: "no-search", searchable: false },
        {
            targets: "trim",
            render: function (data, type, full, meta) {
                if (type === "display") {
                    data = strtrunc(data, 10);
                }

                return data;
            }
        },
        { targets: "date-type", type: "date-eu" }
    ]
});