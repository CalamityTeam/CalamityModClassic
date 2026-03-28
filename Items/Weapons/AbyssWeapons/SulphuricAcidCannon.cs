using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.AbyssWeapons
{
	public class SulphuricAcidCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sulphuric Acid Cannon");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 220;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 90;
	        Item.height = 30;
	        Item.useTime = 18;
	        Item.useAnimation = 18;
            Item.useStyle = 5;
	        Item.noMelee = true;
            Item.knockBack = 6f;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item95;
	        Item.shoot = Mod.Find<ModProjectile>("SulphuricAcidCannon2").Type;
	        Item.shootSpeed = 7f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, 0);
        }
    }
}