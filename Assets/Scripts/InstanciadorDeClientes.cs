using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciadorDeClientes : MonoBehaviour
{
    void Awake()
    {
        int numClientes = QuantidadeDeClientes();
        Debug.Log("Número de Clientes Instanciados: " + numClientes);

        StartCoroutine(GerarClientes(numClientes));
    }

    private IEnumerator GerarClientes(int numClientes)
    {
        for (int i = 0; i < numClientes; i++)
        {
            GameObject aux = new GameObject();
            aux.name = "cliente " + i;
            aux.transform.parent = transform;

            aux.AddComponent<Cliente>();
            Cliente clienteAux = aux.GetComponent<Cliente>();

            clienteAux.cpf = GerarCpf();
            clienteAux.operacoes = OperacaoCliente();
            clienteAux.menorTempo = Tempo();
            clienteAux.maiorTempo = Tempo((int)clienteAux.menorTempo);

            yield return null;
        }
    }
    private int Tempo()
    {
        System.Random rand = new System.Random();
        return rand.Next(1, 10);
    }
    private int Tempo(int tempo)
    {
        System.Random rand = new System.Random();
        return rand.Next((tempo), 11);
    }
    private string GerarCpf()
    {
        int soma = 0, resto = 0;
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        System.Random rand = new System.Random();
        string semente = rand.Next(100000000, 999999999).ToString();

        for (int i = 0; i < 9; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente = semente + resto;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        semente = semente + resto;
        return semente;
    }
    private string OperacaoCliente()
    {
        System.Random rand = new System.Random();
        int num = rand.Next(5, 15);
        bool decisao = false;
        string op = "";
        for (int i = 0; i < num; i++)
        {
            if (!decisao)
            {
                op += rand.Next(0, 99).ToString();
                op += ';';
                decisao = true;
            }
            else
            {
                int operacao = rand.Next(0, 3);

                if (operacao.Equals(0)) op += '+';
                else op += '-';

                op += ';';
                decisao = false;
            }
        }
        return op;
    }
    private int QuantidadeDeClientes()
    {
        System.Random rand = new System.Random();
        return rand.Next(1, 100);
    }
}
