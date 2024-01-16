using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Monsoon : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Monsoon");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Monsoon");
        Item.damage = 63;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 46;
        Item.height = 88;
        Item.useTime = 21;
        Item.useAnimation = 21;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        ////Tooltip.SetDefault("Low chance to fire typhoon arrows\nTyphoons deal MORE damage in Expert Mode\nHigh chance to fire sharks\nFires five arrows at once");
        Item.knockBack = 2.5f;
        Item.value = 1000000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = 1; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 10f;
        Item.useAmmo = 40;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	float num117 = 0.314159274f;
		int num118 = 5;
		Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
		vector7.Normalize();
		vector7 *= 40f;
		bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
		for (int num119 = 0; num119 < num118; num119++)
		{
			float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
			Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
			if (!flag11)
			{
				value9 -= vector7;
			}
			switch (Main.rand.Next(12))
			{
	    		case 1: type = 408; break;
	    		default: break;
			}
			switch (Main.rand.Next(25))
			{
	    		case 1: type = Mod.Find<ModProjectile>("TyphoonArrow").Type; break;
	    		default: break;
			}
                int num121 = Projectile.NewProjectile(source, vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
			Main.projectile[num121].noDropItem = true;
		}
		return false;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.FragmentVortex, 10);
        recipe.AddIngredient(ItemID.Tsunami);
        recipe.AddIngredient(ItemID.SharkFin, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}