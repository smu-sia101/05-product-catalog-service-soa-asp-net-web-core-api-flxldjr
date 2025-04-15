import axios from 'axios';

const API_URL = 'https://localhost:7116/api/Product';

export const getProducts = () => axios.get(API_URL);
export const getProductById = (id) => axios.get(`${API_URL}/${id}`);
export const createProduct = (product) => axios.post(API_URL, product);
export const updateProduct = (product) => axios.put(API_URL, product);
export const deleteProduct = (id) => axios.delete(`${API_URL}/${id}`);
