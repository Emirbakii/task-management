import React, { useEffect, useState } from 'react';
import api from '../services/api';

interface DashboardStats {
    projects: { total: number; active: number };
    tasks: { total: number; completed: number; inProgress: number; overdue: number };
}

const Dashboard: React.FC = () => {
    const [stats, setStats] = useState<DashboardStats | null>(null);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchStats = async () => {
            try {
                // Backend'deki /api/dashboard ucuna istek atıyoruz
                const response = await api.get('/dashboard');
                setStats(response.data);
            } catch (err) {
                setError('Veriler yüklenirken bir hata oluştu.');
            }
        };
        fetchStats();
    }, []);

    if (error) return <p style={{ color: 'red', textAlign: 'center', marginTop: '50px' }}>{error}</p>;
    if (!stats) return <p style={{ textAlign: 'center', marginTop: '50px' }}>Yükleniyor...</p>;

    return (
        <div style={{ maxWidth: '800px', margin: '50px auto', fontFamily: 'sans-serif' }}>
            <h2>ANKA Task Sistem Özeti</h2>
            <div style={{ display: 'flex', gap: '20px', marginTop: '20px' }}>
                <div style={{ flex: 1, border: '1px solid #ddd', padding: '20px', borderRadius: '8px', backgroundColor: '#f9f9f9' }}>
                    <h3 style={{ borderBottom: '2px solid #007bff', paddingBottom: '10px' }}>Projeler</h3>
                    <p><strong>Toplam:</strong> {stats.projects.total}</p>
                    <p><strong>Aktif:</strong> {stats.projects.active}</p>
                </div>
                <div style={{ flex: 1, border: '1px solid #ddd', padding: '20px', borderRadius: '8px', backgroundColor: '#f9f9f9' }}>
                    <h3 style={{ borderBottom: '2px solid #28a745', paddingBottom: '10px' }}>Görevler</h3>
                    <p><strong>Toplam:</strong> {stats.tasks.total}</p>
                    <p><strong>Tamamlanan:</strong> {stats.tasks.completed}</p>
                    <p><strong>Devam Eden:</strong> {stats.tasks.inProgress}</p>
                    <p><strong>Geciken:</strong> {stats.tasks.overdue}</p>
                </div>
            </div>
        </div>
    );
};

export default Dashboard;