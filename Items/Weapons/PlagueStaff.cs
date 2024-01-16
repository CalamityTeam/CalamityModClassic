using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PlagueStaff : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/8.Plaguebringer/PlagueStaff");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Plague Staff");
        Item.damage = 82;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 22;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Fires a spread of plague fangs");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 8;
        Item.value = 1250000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item43;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("PlagueFang").Type;
        Item.shootSpeed = 16f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PlagueCellCluster", 11);
        recipe.AddIngredient(ItemID.VenomStaff);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	int i = Main.myPlayer;
		float num72 = Item.shootSpeed;
		int num73 = Item.damage;
		float num74 = Item.knockBack;
    	float num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
		float num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
		float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
    	int num130 = 6;
		if (Main.rand.Next(3) == 0)
		{
			num130++;
		}
		if (Main.rand.Next(4) == 0)
		{
			num130++;
		}
		if (Main.rand.Next(5) == 0)
		{
			num130++;
		}
		for (int num131 = 0; num131 < num130; num131++)
		{
			float num132 = num78;
			float num133 = num79;
			float num134 = 0.05f * (float)num131;
			num132 += (float)Main.rand.Next(-55, 56) * num134;
			num133 += (float)Main.rand.Next(-55, 56) * num134;
			num80 = (float)Math.Sqrt((double)(num132 * num132 + num133 * num133));
			num80 = num72 / num80;
			num132 *= num80;
			num133 *= num80;
			float x2 = vector2.X;
			float y2 = vector2.Y;
			Projectile.NewProjectile(source, x2, y2, num132, num133, Mod.Find<ModProjectile>("PlagueFang").Type, num73, num74, i, 0f, 0f);
		}
		return false;
	}
}}