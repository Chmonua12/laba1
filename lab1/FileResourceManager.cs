using System;
using System.IO;

namespace Lab1
{
    public class FileResourceManager : IDisposable
    {
        private FileStream? _fileStream;
        private StreamWriter? _writer;
        private StreamReader? _reader;
        private bool _disposed;
        private readonly string _filePath;
        
        public FileResourceManager(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }
        
        public void OpenForWriting()
        {
            CheckDisposed();
            _fileStream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Write);
            _writer = new StreamWriter(_fileStream);
        }
        
        public void OpenForReading()
        {
            CheckDisposed();
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("Файл не найден", _filePath);
                
            _fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            _reader = new StreamReader(_fileStream);
        }
        
        public void WriteLine(string text)
        {
            CheckDisposed();
            if (_writer == null)
                throw new InvalidOperationException("Сначала откройте файл");
                
            _writer.WriteLine(text);
            _writer.Flush(); // Чтобы сразу записалось
        }

        public string ReadAllText()
        {
            CheckDisposed();
            if (_reader == null)
                throw new InvalidOperationException("Сначала откройте файл");
                
            return _reader.ReadToEnd();
        }
        
        public void AppendText(string text)
        {
            CheckDisposed();
            File.AppendAllText(_filePath, text + Environment.NewLine);
        }

        public FileInfo GetFileInfo()
        {
            CheckDisposed();
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("Файл не найден", _filePath);
                
            return new FileInfo(_filePath);
        }
        
        // IDisposable реализация
        private void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(FileResourceManager));
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            
            if (disposing)
            {
                _writer?.Dispose();
                _reader?.Dispose();
                _fileStream?.Dispose();
            }
            
            _disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        ~FileResourceManager()
        {
            Dispose(false);
        }
    }
}