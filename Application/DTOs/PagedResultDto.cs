namespace Application.DTOs
{
    /// <summary>
    /// Objeto genérico utilizado para encapsular respuestas paginadas.
    /// Proporciona los datos solicitados junto con metadatos de navegación de página.
    /// </summary>
    /// <typeparam name="T">El tipo de los elementos contenidos en la página actual.</typeparam>
    public class PagedResultDto<T>
    {
        /// <summary>
        /// La colección de elementos de la página actual.
        /// </summary>
        public required IEnumerable<T> Data { get; set; }

        /// <summary>
        /// El número de la página actual (índice basado en 1).
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// La cantidad máxima de registros solicitados por página.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// El número total de registros existentes en la base de datos que coinciden con la consulta sin aplicar paginación.
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// El número total de páginas disponibles, calculado a partir de los registros totales y el tamaño de página.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);

        /// <summary>
        /// Indica si existe una página posterior a la actual.
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// Indica si existe una página previa a la actual.
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;
    }
}