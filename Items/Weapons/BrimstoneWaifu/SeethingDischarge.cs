using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.BrimstoneWaifu
{
	public class SeethingDischarge : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Seething Discharge");
			//Tooltip.SetDefault("Fires a barrage of brimstone blasts");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 62;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 15;
			Item.width = 28;
			Item.height = 32;
			Item.useTime = 24;
			Item.useAnimation = 24;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 6.75f;
			Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point2/Sounds/Item/FlareSound");
			Item.value = 500000;
			Item.rare = ItemRarityID.Pink;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("BrimstoneBarrage").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 6f;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        float SpeedX = velocity.X + 10f * 0.05f;
	        float SpeedY = velocity.Y + 10f * 0.05f;
	        float SpeedX2 = velocity.X - 10f * 0.05f;
	        float SpeedY2 = velocity.Y - 10f * 0.05f;
	        float SpeedX3 = velocity.X + 0f * 0.05f;
	        float SpeedY3 = velocity.Y + 0f * 0.05f;
	        int projectile1 = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        int projectile2 = Projectile.NewProjectile(source, position.X, position.Y, SpeedX2, SpeedY2, Mod.Find<ModProjectile>("BrimstoneHellblast").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        int projectile3 = Projectile.NewProjectile(source, position.X, position.Y, SpeedX3, SpeedY3, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	        Main.projectile[projectile1].hostile = false;
	        Main.projectile[projectile1].friendly = true;
	        Main.projectile[projectile1].DamageType = DamageClass.Magic;
	        Main.projectile[projectile2].hostile = false;
	        Main.projectile[projectile2].friendly = true;
	        Main.projectile[projectile2].tileCollide = false;
	        Main.projectile[projectile2].DamageType = DamageClass.Magic;
	        Main.projectile[projectile3].hostile = false;
	        Main.projectile[projectile3].friendly = true;
	        Main.projectile[projectile3].DamageType = DamageClass.Magic;
	    	return false;
		}
	}
}