using AutoMapper;
using System;
using System.Linq;
using WebFoodbornApi.Models;
using WebFoodbornApi.Dtos;
using WebFoodbornApi.Common;

namespace WebFoodbornApi.AutoMapper
{
    public class AutoMapperProfileConfiguraion : Profile
    {
        public AutoMapperProfileConfiguraion()
           : this("MyProfile")
        {

        }

        public AutoMapperProfileConfiguraion(string profileName) : base(profileName)
        {
            CreateMap<User, UserOutput>();
            CreateMap<UserCreateInput, User>()
                .ForMember(user => user.PassWord, option => option.MapFrom(input => Encrypt.Md5Encrypt(input.PassWord)));
            CreateMap<User, UserUpdateInput>();
            CreateMap<User, UserSelectOutput>();

            CreateMap<Role, RoleOutput>();
            CreateMap<RoleCreateInput, Role>();
            CreateMap<Role, RoleUpdateInput>();

            CreateMap<Orgnazition, OrgOutput>();
            CreateMap<OrgCreateInput, Orgnazition>();
            CreateMap<Orgnazition, OrgUpdateInput>();
            CreateMap<Orgnazition, OrgTreeOutput>();

            CreateMap<Permission, PermissionOutput>();
            CreateMap<PermissionCreateInput, Permission>();
            CreateMap<Permission, PermissionUpdateInput>();
            CreateMap<Permission, PermissionTreeOutput>();

            CreateMap<RolePermission, PermissionMenuOutput>();

            CreateMap<Dictionary, DictionaryOutput>();
            CreateMap<DictionaryCreateInput, Dictionary>();
            CreateMap<Dictionary, DictonaryUpdateInput>();
            CreateMap<Dictionary, DictionarySelectOutput>();

            CreateMap<Patient, PatientOutput>();
            CreateMap<PatientCreateInput, Patient>()
                .ForMember(patient => patient.Guid, option => option.MapFrom(input => Guid.NewGuid().ToString("N")));
            CreateMap<Patient, PatientUpdateInput>();

            CreateMap<InitialDiagnosis, InitialDiagnosisOutput>();
            CreateMap<InitialDiagnosisCreateInput, InitialDiagnosis>();
            CreateMap<InitialDiagnosis, InitialDiagnosisUpdateInput>();

            CreateMap<PastMedicalHistory, PastMedicalHistoryOutput>();
            CreateMap<PastMedicalHistoryCreateInput, PastMedicalHistory>();
            CreateMap<PastMedicalHistory, PastMedicalHistoryUpdateInput>();

            CreateMap<Symptom, SymptomOutput>();
            CreateMap<SymptomCreateInput, Symptom>();
            CreateMap<Symptom, SymptomUpdateInput>();

            CreateMap<FoodInfo, FoodInfoOutput>();
            CreateMap<FoodInfoCreateInput, FoodInfo>();
            CreateMap<FoodInfo, FoodInfoUpdateInput>();
        }
    }
}
