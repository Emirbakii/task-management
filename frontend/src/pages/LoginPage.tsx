import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

const LoginPage: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await api.post('/auth/login', { email, password });
            
            // Backend'den gelen token'ı tarayıcıya kaydediyoruz
            localStorage.setItem('token', response.data.token);
            
            // Giriş başarılıysa Dashboard'a yönlendir
            navigate('/dashboard');
        } catch (err) {
            setError('Giriş başarısız. Lütfen bilgilerinizi kontrol edin.');
        }
    };

    return (
        <div style={{ maxWidth: '400px', margin: '100px auto', fontFamily: 'sans-serif' }}>
            <h2>ANKA Task Giriş</h2>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            
            <form onSubmit={handleLogin} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
                <input 
                    type="email" 
                    placeholder="E-posta" 
                    value={email} 
                    onChange={(e) => setEmail(e.target.value)} 
                    required 
                    style={{ padding: '8px' }}
                />
                <input 
                    type="password" 
                    placeholder="Şifre" 
                    value={password} 
                    onChange={(e) => setPassword(e.target.value)} 
                    required 
                    style={{ padding: '8px' }}
                />
                <button type="submit" style={{ padding: '10px', background: '#007bff', color: 'white', border: 'none', cursor: 'pointer' }}>
                    Giriş Yap
                </button>
            </form>
        </div>
    );
};

export default LoginPage;