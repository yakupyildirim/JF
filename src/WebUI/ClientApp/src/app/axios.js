import axios from 'axios'

const baseURL = "https://localhost:5001";
let headers = {
	'Access-Control-Allow-Origin': '*',
};

const axiosInstance = axios.create({
	baseURL: baseURL,
	headers: headers
});

export default axiosInstance;