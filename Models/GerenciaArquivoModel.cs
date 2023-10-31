namespace LaDoces2.Models
{
    public class GerenciaArquivoModel
    {
        public FileInfo[] File { get; set; }
        public IFormFile IFormFile { get; set; }
        public List<IFormFile> IFormFiles { get; set; }
        public string PathImagemItem { get; set; }
    }
}