using PPadillaT5.Models;
namespace PPadillaT5.Vistas;

public partial class vPrincipal : ContentPage
{
	public vPrincipal()
	{
		InitializeComponent();
	}

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        eliminar();
        mostrar();
    }

    private void btnEditar_Clicked(object sender, EventArgs e)
    {
        editar();
        mostrar();
    }

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        insertar();
        mostrar();
        limpiar();
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        mostrar();
        limpiar();
    }
public void insertar()
    {
     
        App.personRepo.AddNewPerson(txtName.Text);
        lblMensaje.Text = App.personRepo.statusMessage;
    }
    public void mostrar()
    {

        List<Persona> people = App.personRepo.GetAllPeople();
        listPerson.ItemsSource = people;
    }
    public void editar()
    {
        try
        {
            if (int.TryParse(txtId.Text, out int personId))
            {
                App.personRepo.UpdatePerson(personId, txtName.Text);


                lblMensaje.Text = App.personRepo.statusMessage;
                limpiar();
            }
            else
            {
                lblMensaje.Text = "Ingrese un ID válido";
            }
        }
        catch (Exception ex)
        {
            lblMensaje.Text = $"Error al intentar actualizar: {ex.Message}";
        }
    }
    public void eliminar()
    {

        try
        {
            //int personId = Convert.ToInt32(txtName.Text);
            if (int.TryParse(txtId.Text, out int personId))
            {

                App.personRepo.DeletePerson(personId);
                lblMensaje.Text = App.personRepo.statusMessage;
                limpiar();

            }
            else
            {
                // El valor de txtName.Text no es un entero válido
                // Realiza la lógica correspondiente o muestra un mensaje de error
                lblMensaje.Text = "Ingrese un valor entero válido en el campo";
            }


        }
        catch (Exception ex)
        {

            lblMensaje.Text = $"Error al intentar eliminar: {ex.Message}";
        }


    }
    private void limpiar()
    {
        txtId.Text = "";
        txtName.Text = "";
    }
}