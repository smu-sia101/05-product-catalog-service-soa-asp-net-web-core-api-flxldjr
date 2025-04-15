import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const CreateProduct = () => {
const navigate = useNavigate();
const [product, setProduct] = useState({
    name: '',
    price: '',
    description: '',
    category: '',
    stock: '',
    imageUrl: '',
});

const handleChange = (e) => {
    const { name, value } = e.target;
    setProduct((prevProduct) => ({
    ...prevProduct,
    [name]: value
    }));
};

const handleSubmit = async (e) => {
    e.preventDefault();

    console.log("Submitting product:", product);

    try {
    const payload = {
        name: product.name.trim(),
        price: parseFloat(product.price),
        description: product.description.trim(),
        category: product.category.trim(),
        stock: parseInt(product.stock),
        imageUrl: product.imageUrl.trim(),
    };


    console.log("Payload being sent:", payload);

    await axios.post("https://localhost:7116/api/Product", payload);

    navigate('/');
    } catch (error) {
    if (error.response && error.response.data && error.response.data.errors) {
        console.error("Validation Errors:", error.response.data.errors);
    } else {
        console.error("API Error:", error.message);
    }
    }
};



return (
    <form onSubmit={handleSubmit}>
    <h2>Create Product</h2>
    {Object.keys(product).map(key => (
        <div key={key}>
        <input
            name={key}
            placeholder={key}
            value={product[key]}
            onChange={handleChange}
        />
        </div>
    ))}
    <button type="submit">Create</button>
    </form>
);
};

export default CreateProduct;
