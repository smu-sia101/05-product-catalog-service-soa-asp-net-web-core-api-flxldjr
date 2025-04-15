import React, { useEffect, useState } from 'react';
import { getProductById, updateProduct } from '../api/productApi';
import { useParams, useNavigate } from 'react-router-dom';

const EditProduct = () => {
const { id } = useParams();
const navigate = useNavigate();
const [product, setProduct] = useState(null);

useEffect(() => {
    getProductById(id).then(res => setProduct(res.data));
}, [id]);

const handleChange = e => {
    setProduct({ ...product, [e.target.name]: e.target.value });
};

const handleSubmit = async e => {
    e.preventDefault();
    await updateProduct(product);
    navigate('/');
};

if (!product) return <p>Loading...</p>;

return (
    <form onSubmit={handleSubmit}>
    <h2>Edit Product</h2>
    {Object.keys(product).map(key => (
        key !== 'id' && (
        <div key={key}>
            <input
            name={key}
            placeholder={key}
            value={product[key]}
            onChange={handleChange}
            />
        </div>
        )
    ))}
    <button type="submit">Update</button>
    </form>
);
};

export default EditProduct;
