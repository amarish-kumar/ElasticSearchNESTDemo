using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Fonlow.Demo.Contracts
{
    public class Constants
    {
        public const string ServiceNamespace = "http://fonlow.com/Demo/2015/01";
        public const string DataNamespace = "http://fonlow.com/Demo/Data/2015/01";
        public const string DefaultIndex = "fonlow_es_demo";
    }


    [DataContract(Namespace = Constants.DataNamespace)]
    public class Entity
    {
        public Entity()
        {
            Addresses = new ObservableCollection<Address>();
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }


        [DataMember]
        public virtual ObservableCollection<Address> Addresses { get; set; }

        [DataMember]
        public string EmailAddress
        {
            get;
            set;
        }

        [DataMember]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity;
            if (entity == null)
            {
                return false;
            }

            return Id == entity.Id && Name == entity.Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    [DataContract(Namespace = Constants.DataNamespace)]
    public class Person : Entity
    {
        public Person()
        {
        }

        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public string GivenName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        /// <summary>
        /// Optional birthdate
        /// </summary>
        [DataMember]
        [DisplayName("Birthdate")]
        public DateTime? BirthDate { get; set; }


        [DataMember]
        public string Position { get; set; }

    }

    public class Company : Entity
    {
        public string BusinessNumber { get; set; }

        public string BusinessNumberType { get; set; }
    }


    [DataContract(Namespace = Constants.DataNamespace)]
    public enum AddressType
    {
        [EnumMember]
        Undefined = 0,
        [EnumMember]
        Geo = 1,
        [EnumMember]
        Postal = 2,
        [EnumMember]
        Delivery = 4,
        [EnumMember]
        Home = 8
    }


    /// <summary>
    /// Common structure used by tables. Better not to have its own table.
    /// </summary>
    [DataContract(Namespace = Constants.DataNamespace)]
    public class Address
    {
        public Guid Id { get; set; }

        [DataMember]
        public string FullAddress { get; set; }


        [DataMember]
        public string Street1 { get; set; }
        [DataMember]
        public string Street2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public AddressType AddressType { get; set; }

        /// <summary>
        /// Foreign key to Entity defined in Fluent
        /// </summary>
        public Guid EntityId { get; set; }
    }


    public class MediaFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        //public MediaType Type {get;set;}
    }

}
