using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Banco : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelWait;
    public GameObject panelGame;
    public Text[] camposDeTexto;

    [Header("Config")]
    public GameObject listaClientes;
    public float tempoTotal;

    private Queue<Cliente> clientes = new Queue<Cliente>();

    private void Start()
    {
        Invoke("IniciarQueue", 2f);
    }
    private void IniciarQueue()
    {
        List<Cliente> aux = new List<Cliente>();
        PegarComponentFilhos(aux, 0);
        foreach (var item in aux)
        {
            clientes.Enqueue(item);
        }

        panelWait.SetActive(false);
        panelGame.SetActive(true);
    }

    // Questao 1
    public void PegarComponentFilhos(List<Cliente> list, int index)
    {
        if (index >= listaClientes.transform.childCount)
            return;

        list.Add(listaClientes.transform.GetChild(index).GetComponent<Cliente>());
        PegarComponentFilhos(list, index + 1);
    }

    public void AtenderCliente()
    {
        if(clientes.Count > 0)
        {
            Cliente clienteDaVez = clientes.Dequeue();
            camposDeTexto[0].text = string.Format("Cpf: {0}", clienteDaVez.cpf);
            camposDeTexto[1].text = string.Format("Tempo min cliente: {0} | Tempo máx cliente: {1}", clienteDaVez.menorTempo, clienteDaVez.maiorTempo);

            int tempo = (int)((clienteDaVez.maiorTempo + clienteDaVez.menorTempo) / 2);
            tempoTotal += tempo;
            camposDeTexto[2].text = string.Format("Tempo Gasto: {0}", tempo);

            string[] aux = clienteDaVez.operacoes.Split(';');
            Queue<string> operacoesDaVez = new Queue<string>();
            foreach (var item in aux)
            {
                operacoesDaVez.Enqueue(item);
            }

            int total = 0;
            int ultmNum = 0;
            string operacoes = "";
            while (operacoesDaVez.Count > 0)
            {
                string xua = operacoesDaVez.Dequeue();

                if (xua.Equals("+"))
                    total += ultmNum;
                else if (xua.Equals("-"))
                    total -= ultmNum;
                else if (xua != " " && xua != "")
                    ultmNum = int.Parse(xua);

                operacoes += xua + " ";
            }

            camposDeTexto[3].text = string.Format("Operações: {0}= {1}", operacoes, total.ToString());

            Destroy(clienteDaVez.gameObject);
        }
        else camposDeTexto[4].text = "Não há mais clientes";
    }

    public void ClientesRestantes()
    {
        if (clientes.Count > 0) camposDeTexto[4].text = string.Format("Restam {0} clientes. Tempo total gasto: {1} seg", clientes.Count, tempoTotal);
        else camposDeTexto[4].text = "Não há mais clientes";
    }

    public void ImprimirEmDebug()
    {
        Cliente[] aux = new Cliente[clientes.Count];
        clientes.CopyTo(aux, 0);

        for (int i = 0; i < aux.Length; i++)
        {
            Debug.Log(string.Format("{0} | CPF {1}", aux[i].name, aux[i].cpf));
        }

        aux = null;
    }
}
