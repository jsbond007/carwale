import axios from 'axios'
import { useNavigate } from 'react-router-dom';
import { toast } from "react-toastify";
import { Environment } from './environment';


const url = Environment.getUrl();
// Add a request interceptor
axios.interceptors.request.use(
	config => {
		const token = localStorage.getItem('token');
		if (token) {
			config.headers['Authorization'] = 'Bearer ' + token
		}
		// config.headers['Content-Type'] = 'application/json';
		return config
	},
	error => {
		debugger
		Promise.reject(error)
	}
);

axios.interceptors.response.use(
	function (response) {
		// document.body.classList.remove("loading-indicator");
		// Any status code that lie within the range of 2xx cause this function to trigger
		// Do something with response data
		return response;
	},
	function (error) {
		// Any status codes that falls outside the range of 2xx cause this function to trigger
		// Do something with response error

		// Internal server error
		if (error.response.status === 500) {
			toast.error(error.response.data);
		}
		// Not found
		if (error.response.status === 404) {
		}
		// Unauthorize
		if (error.response.status === 401) {
			localStorage.removeItem("token");

			const navigate = useNavigate();
			navigate("/login");
			toast.error(error.response.message);
		}

		document.body.classList.remove("loading-indicator");
		return Promise.reject(error);
	}
);