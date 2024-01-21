namespace PPadillaT5.Vistas;

public partial class Eliminar : ContentPage
{
	public Eliminar()
	{
		InitializeComponent();
	}

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (int.TryParse(PersonId.Text, out int personId))
            {
                // Crear una instancia del repositorio
                PersonRepository personRepository = new PersonRepository("ruta_de_tu_base_de_datos");

                // Llamar al método DeletePerson para eliminar la persona
                personRepository.DeletePerson(personId);

                // Actualizar la etiqueta de mensaje
                lblEliminarMensaje.Text = personRepository.statusMessage;
            }
            else
            {
                lblEliminarMensaje.Text = "Ingrese un ID válido";
            }
        }
        catch (Exception ex)
        {
            lblEliminarMensaje.Text = $"Error al intentar eliminar: {ex.Message}";
        }
    }
}