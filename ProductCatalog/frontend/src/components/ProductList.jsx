import React, { useEffect, useState } from 'react';
import { getProducts, deleteProduct } from '../api/productApi';
import { Link } from 'react-router-dom';
import './ProductList.css'; 

const ProductList = () => {
const [products, setProducts] = useState([]);

const loadProducts = async () => {
    const res = await getProducts();
    setProducts(res.data);
};

const handleDelete = async (id) => {
await deleteProduct(id);
    loadProducts();
};

useEffect(() => {
    loadProducts();
}, []);

return (
    <div className="container">
    <div className="header">
        <h2>Product List</h2>
    </div>
    <Link to="/create" className="create-btn">+ Create Product</Link>
    <div className="product-grid">
        {products.map(p => (
        <div className="product-card" key={p.id}>
            {p.imageUrl && (
            <img src={p.imageUrl} alt={p.name} className="product-image" />
            )}
            <div className="product-details">
            <h3>{p.name}</h3>
            <p><strong>Price:</strong> ${p.price}</p>
            <p><strong>Category:</strong> {p.category}</p>
            <p><strong>Stock:</strong> {p.stock}</p>
            <p>{p.description}</p>
            <div className="card-actions">
                <Link to={`/edit/${p.id}`} className="edit-btn">Edit</Link>
                <button onClick={() => handleDelete(p.id)} className="delete-btn">Delete</button>
            </div>
            </div>
        </div>
        ))}
    </div>
    </div>
);
};

export default ProductList;
