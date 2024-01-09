using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TheMutilator : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 60;  //The width of the .png file in pixels divided by 2.
		Item.damage = 380;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 15;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 15;
		Item.useTurn = true;
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 60;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1000000;  //Value is calculated in copper coins.
		Item.shootSpeed = 10f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 0);
            }
        }
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	if (target.life <= (target.lifeMax * 0.1f))
    	{
    		int heartDrop = Main.rand.Next(2, 4);
			for (int i = 0; i < heartDrop; i++)
			{
				Item.NewItem(player.GetSource_FromThis(), (int)target.position.X, (int)target.position.Y, target.width, target.height, 58, 1, false, 0, false, false);
			}
    		SoundEngine.PlaySound(SoundID.Item14, target.position);
    		target.position.X = target.position.X + (float)(target.width / 2);
			target.position.Y = target.position.Y + (float)(target.height / 2);
			target.position.X = target.position.X - (float)(target.width / 2);
			target.position.Y = target.position.Y - (float)(target.height / 2);
			for (int num621 = 0; num621 < 30; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, DustID.Blood, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.NextBool(2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 50; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, DustID.Blood, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, DustID.Blood, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
    	}
	}
    
    public override void AddRecipes()
	{
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BloodstoneCore", 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}
