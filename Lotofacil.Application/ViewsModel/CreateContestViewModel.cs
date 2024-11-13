using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.ViewsModel
{
    public class CreateContestViewModel //Serve para as duas entidades: Contest e BaseContest
    {
        public string Name { get; set; }

        public DateTime Data { get; set; }

        public string Numbers { get; set; }
    }
}
/*Inclua o Id quando ele for necessário para a operação: 
-Se a View exibir detalhes específicos da entidade, ou se o Id for necessário para operações como edição, exclusão ou identificação única no banco, ele deve estar na ViewModel.
Oculte o Id se ele não for necessário: 
-Em operações onde o Id não é usado, como formulários de criação, ou em situações onde o identificador não precisa ser exposto ao usuário, você pode omiti-lo.
*/

