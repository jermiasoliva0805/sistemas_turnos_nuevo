export const formatDate = (date: Date): string => {
    return date.toISOString().split('T')[0];
};

export const formatTime = (time: string): string => {
    return time.substring(0, 5);
};

export const formatDateTime = (dateStr: string): string => {
    const date = new Date(dateStr);
    return date.toLocaleDateString('es-AR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
    });
};