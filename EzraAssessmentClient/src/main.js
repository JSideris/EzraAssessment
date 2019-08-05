import Vue from 'vue';
import Home from './pages/Home/Home.vue';
import AddEmployee from './pages/Add/AddEmployee.vue';


Vue.config.productionTip = true;

new Vue({
	render: h => h({
		"/": Home,
		"/add": AddEmployee
	}[window.location.pathname.toLowerCase()])
}).$mount('#app');
