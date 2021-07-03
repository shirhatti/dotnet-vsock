using System.Runtime.InteropServices;
using Tmds.Linux;

namespace WebApplicationVSock
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct sockaddr_vm
    {
        [FieldOffset(0)]
        public sa_family_t sa_family;
        [FieldOffset(2)]
        public ushort svm_reserved1;
        [FieldOffset(4)]
        public uint svm_port;      /* Port # in host byte order */
        [FieldOffset(8)]
        public uint svm_cid;       /* Address in host byte order */
        [FieldOffset(12)]
        private fixed byte _sa_data[4];
    }
}
