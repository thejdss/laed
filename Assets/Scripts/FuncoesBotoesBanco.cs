using UnityEngine;

public class FuncoesBotoesBanco : MonoBehaviour
{
    public Banco bancoAtual;

    public void ChamarCliente()
    {
        bancoAtual.AtenderCliente();
    }

    public void MostrarClientesRestantes()
    {
        bancoAtual.ClientesRestantes();
    }

    public void ImprimirCliente()
    {
        bancoAtual.ImprimirEmDebug();
    }
}
