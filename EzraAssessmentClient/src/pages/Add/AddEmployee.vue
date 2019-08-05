<template>
    <div class="home">
        <h1>Add Employee</h1>
        <div><div id="error-out" style="display:none;"></div></div>
        <div class="label-container"><label for="name-in">Name: </label></div><input id="name-in" v-model="employee.name" /><br /><br />
        <div class="label-container"><label for="email-in">Email: </label></div><input id="email-in" v-model="employee.email" /><br /><br />
        <div class="label-container"><label for="phone-number-in">Phone Number: </label></div><input id="phone-number-in" v-model="employee.phoneNumber" /><br /><br />
        <button @click="save()">Add</button>
        <button @click="cancel()">Cancel</button>
    </div>
</template>

<script>
    var employees = [];

    import JQuery from 'jquery'
    let $ = JQuery

    export default {
        name: 'AddEmployee',
        data: function () {
            return {
                employee: {
                    name: "",
                    email: "",
                    phoneNumber: ""
                }
            };
        },
        methods: {
            save: function () {
                fetch("http://localhost:5000/api/Employees/", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(this.employee)
                }).then((e) => {
                    if (!e.ok) {
                        this.saveFailed(e);
                    }
                    else {
                        window.location.href = "/";
                    }
                }).catch((e) => {
                    this.saveFailed(e);
                })
            },
            cancel: function () {
                // Return home without saving.

                window.location.href = "/";
            },
            saveFailed: function () {
                $("#error-out").html("All fields are required.").show(200);
            }
        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    * {
        font-family: Arial, Helvetica, sans-serif;
    }

    button {
        margin-right: 100px;
    }

    .label-container {
        display: inline-block;
        width: 150px;
    }

    #error-out {
        color: red;
        background-color: #FAA;
        border: 1px solid red;
        padding: 5px;
        display: inline-block;
        margin-bottom: 10px;
    }

</style>
