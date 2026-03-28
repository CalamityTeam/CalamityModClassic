using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.SunkenSea
{
    public class EutrophicScimitar : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eutrophic Scimitar");
			// Tooltip.SetDefault("Fires a beam upon swing that stuns enemies");
		}

		public override void SetDefaults()
		{
			Item.damage = 130;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 46;
			Item.height = 76;
			Item.useTime = 38;
			Item.useAnimation = 38;
			Item.useStyle = 1;
			Item.knockBack = 2;
			Item.shoot = Mod.Find<ModProjectile>("EutrophicScimitarProj").Type;
			Item.shootSpeed = 17;
			Item.value = Item.buyPrice(0, 36, 0, 0);
			Item.rare = 5;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i <= 21; i++)
            {
                Dust dust;
                dust = Main.dust[Terraria.Dust.NewDust(new Vector2(position.X - 58 / 2, position.Y - 58 / 2), 58, 58, 226, 0f, 0f, 0, new Color(255, 255, 255), 0.4605263f)];
                dust.noGravity = true;
                dust.fadeIn = 0.9473684f;
            }
            return true;
        }
    }
}
