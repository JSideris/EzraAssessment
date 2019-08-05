<template>
    <div class="home">
        <h1>Employee List</h1>
        <a @click="visitAddEmployeePage()">Insert</a>
        <!--<router-link to="/AddEmployee">Add</router-link>-->
        <table>
            <tbody>
                <tr>
                    <th>Employee #</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Phone Number</th>
                    <th colspan="2"></th>
                </tr>
                <tr v-for="employee in employees" v-bind:key="employee.id" :id="'employee-row-' + employee.id">
                    <td>{{ employee.id }}</td>
                    <td><span>{{ employee.name }}</span><input style="display:none;" v-model="employee.name" /></td>
                    <td><span>{{ employee.email }}</span><input style="display:none;" v-model="employee.email" /></td>
                    <td><span>{{ employee.phoneNumber }}</span><input style="display:none;" v-model="employee.phoneNumber" /></td>
                    <td><a class="modify-btn" :data-id="employee.id" @click="modifyEmployee(arguments[0])">(modify)</a> <a class="save-edits-btn" :data-id="employee.id" style="display:none;" @click="doneEdit(arguments[0])">(done)</a></td>
                    <td><a class="delete-btn" :data-id="employee.id" @click="deleteEmployee(arguments[0])">(delete)</a></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    var employees = [];

    import JQuery from 'jquery'
    let $ = JQuery

    export default {
        name: 'Home',
        data: function () {
            return { employees: employees };
        },
        methods: {
            deleteEmployee: function (e) {
                let id = e.target.getAttribute("data-id");
                fetch("http://localhost:5000/api/Employees/" + id, {
                    method: "DELETE"
                }).then(() => {
                    this.employees.splice(this.employees.findIndex(e => e.id == id), 1);
                })
            },
            modifyEmployee: function (e) {
                // Allows the row to be edited.

                let id = e.target.getAttribute("data-id");
                let row = document.getElementById(`employee-row-${id}`);
                $(row.querySelectorAll(".modify-btn")[0]).hide();
                $(row.querySelectorAll(".delete-btn")[0]).hide();
                $(row.querySelectorAll(".save-edits-btn")[0]).show();

                $(`#employee-row-${id} span`).hide();
                $(`#employee-row-${id} input`).show();
            },
            doneEdit: function (e) {
                // Saves the edited row to the server.

                let id = e.target.getAttribute("data-id");
                let employee = this.employees[this.employees.findIndex(e => e.id == id)];
                //console.log(employee);
                let row = document.getElementById(`employee-row-${id}`);
                fetch("http://localhost:5000/api/Employees/" + id, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(employee)
                }).then(() => {
                    $(row.querySelectorAll(".save-edits-btn")[0]).hide();
                    $(row.querySelectorAll(".modify-btn")[0]).show();
                    $(row.querySelectorAll(".delete-btn")[0]).show();

                    $(`#employee-row-${id} input`).hide();
                    $(`#employee-row-${id} span`).show();
                })
            },
            visitAddEmployeePage: function () {
                // Navigates to the add employee page.

                window.location.href = ("/add");
            }
        },
        created() {
            fetch("http://localhost:5000/api/Employees")
                .then(response => response.json())
                .then(json => { this.employees = json })
        },
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

    * {
        font-family: Arial, Helvetica, sans-serif;
    }

    th {
        background: #FFF;
        padding: 3px;
    }

    td {
        padding: 3px;
    }

    tr:nth-child(even) {
        background: #EFE
    }

    tr:nth-child(odd) {
        background: #DFD
    }

    a {
        cursor: pointer;
        color: #44F;
    }
</style>
